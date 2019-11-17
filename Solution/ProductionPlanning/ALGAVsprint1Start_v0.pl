:- dynamic operacoes_atrib_maq/2.
:- dynamic classif_operacoes/2.
:- dynamic op_prod_client/9.
:- dynamic operacoes/1.

% FABRICA

% Linhas
linhas([lA]).


% Maquinas


maquinas([ma]).



% Ferramentas


ferramentas([fa,fa,fc]).


% Maquinas que constituem as Linhas

tipos_maq_linha(lA,[ma]).
% ...


% Operações

tipo_operacoes([opt1,opt2,opt3]).

% operacoes/1 deve ser criado dinamicamente
operacoes([op1,op2,op3,op4,op5]).

%operacoes_atrib_maq depois deve ser criado dinamicamente
operacoes_atrib_maq(ma,[op1,op2,op3,op4,op5]).

% classif_operacoes/2 deve ser criado dinamicamente %%atomic_concat(op,NumOp,Resultado)
classif_operacoes(op1,opt1).
classif_operacoes(op2,opt2).
classif_operacoes(op3,opt1).
classif_operacoes(op4,opt2).
classif_operacoes(op5,opt3).
% ...


% Afetação de tipos de operaçoes a tipos de maquinas
% com ferramentas, tempos de setup e tempos de execucao)

operacao_maquina(opt1,ma,fa,5,60).
operacao_maquina(opt2,ma,fb,6,30).
operacao_maquina(opt3,ma,fc,8,40).
%...


% PRODUTOS

produtos([pA,pB,pC]).

operacoes_produto(pA,[opt1]).
operacoes_produto(pB,[opt2]).
operacoes_produto(pC,[opt3]).



% ENCOMENDAS

%Clientes

clientes([clA,clB]).


% prioridades dos clientes

prioridade_cliente(clA,1).
prioridade_cliente(clB,2).
% ...

% Encomendas do cliente, 
% termos e(<produto>,<n.unidades>,<tempo_conclusao>)

encomenda(clA,[e(pA,1,100),e(pB,1,100)]).
encomenda(clB,[e(pA,1,110),e(pB,1,150),e(pC,1,300)]).
% ...


% Separar posteriormente em varios ficheiros



% melhor escalonamento com FINDALL, gera todas as solucoes e escolhe melhor

melhor_escalonamento(M,Lm,Tm):-
	get_time(Ti),
	findall(p(LP,Tempo), 
	permuta_tempo(M,LP,Tempo), LL),
	melhor_permuta(LL,Lm,Tm),
	get_time(Tf),Tcomp is Tf-Ti,
	write('GERADO EM '),write(Tcomp),
	write(' SEGUNDOS'),nl.

% 1- HEURÍSTICA DE MINIMIZAÇÃO DE TEMPO DE OCUPAÇÃO (Visa minimizar tempos de Setup)

% permuta/2 gera permutações de listas
permuta([ ],[ ]).
permuta(L,[X|L1]):-apaga1(X,L,Li),permuta(Li,L1).

apaga1(X,[X|L],L).
apaga1(X,[Y|L],[Y|L1]):-apaga1(X,L,L1).

% permuta_tempo/3 faz uma permutação das operações atribuídas a uma maquina 
% e calcula tempo de ocupação incluindo trocas de ferramentas
permuta_tempo(M,LP,Tempo):- operacoes_atrib_maq(M,L),
permuta(L,LP),soma_tempos(semfer,M,LP,Tempo).

%Calcular o tempo para cada possibilidade de sequenciamento
soma_tempos(_,_,[],0).
soma_tempos(Fer,M,[Op|LOp],Tempo):- classif_operacoes(Op,Opt),
	operacao_maquina(Opt,M,Fer1,Tsetup,Texec),
	soma_tempos(Fer1,M,LOp,Tempo1),
	((Fer1==Fer,!,Tempo is Texec+Tempo1);
			Tempo is Tsetup+Texec+Tempo1),
			write('FERRAMENTA1 '),write(Fer1), write(' FERRAMENTA '),write(Fer),
	write(' Operation '),write(Op),
	write(' TempoAcumu '), write(Tempo),nl.
%Seleciona a melhor permuta, considerando o menor somatório dos tAtraso e o menor tOcupacao
melhor_permuta([p(LP,Tempo)],LP,Tempo):-!.
melhor_permuta([p(LP,Tempo)|LL],LPm,Tm):- melhor_permuta(LL,LP1,T1),
		((Tempo<T1,!,Tm is Tempo,LPm=LP);(Tm is T1,LPm=LP1)).


% ----------------------------------------------------------

% 2 - Melhor Escalonamento por geração de todas as soluções para a minimização do somatório dos tempos de atraso
permuta_tempo_atraso(M,LP,TAtraso):-  
	menor_tempo_conclusao(List), 
	length(List,Length), 
    soma_tempos_atraso(semfer,M,[], 0, 0, List, 0,Length, T, LA),
	!,
	TAtraso is T, LP = LA.

menor_tempo_conclusao(Lista) :- 
	findall(E, encomenda(_,E), L),
	flatten(L,List),
	qsort(List,Lis),
	reverse(Lis,Lista).

qsort(Lista, Final):-
	qsort(Lista,[],Final).
qsort([],A,A).
qsort(List,List2,Final):- 
	menor_tempo_c(List,Menor),[_|T] = List,qsort(T,[Menor|List2],Final).

menor_tempo_c([e(_,_,T1)],T1):- 
	!.
menor_tempo_c([e(P1,Q1,T1), e(P2,_,T2)|T],M):- 
	((T1=T2,P1=P2);T1<T2), 
	menor_tempo_c([e(P1,Q1,T1)|T], M).
menor_tempo_c([e(P1,_,T1), e(P2,Q2,T2)|T],M):- 
	(T1>T2;(T1=T2,P1\=P2)), 
	menor_tempo_c([e(P2,Q2,T2)|T], M).

soma_tempos_atraso(_,_,L,T,_,_,Length,Length, T,L).
soma_tempos_atraso(Fer,M,LA,SomatorioTempoAtraso,SomaOcup,Lista, Count,Length, T, L):- 
	nth0(Count,Lista,e(P,_,TConc)),
	Count1 is Count+1,
	operacoes_produto(P, Oper),
	operacao_maquina(Oper,M,Fer1,Tsetup,Texec),
	((Fer1\=Fer,SomaOcup1 is SomaOcup+Texec+Tsetup);
	(SomaOcup1 is Texec+SomaOcup )),
	((TConc < SomaOcup1,SomaAteAgora is SomatorioTempoAtraso+(SomaOcup1-TConc));
	(Count == 0, SomaAteAgora is 0);
	(SomaAteAgora is SomatorioTempoAtraso )),
	soma_tempos_atraso(Fer1,M,[Oper|LA],SomaAteAgora, SomaOcup1, Lista, Count1,Length, T, L).

melhor_escalonamento_findall(M,La,Ta):-
	get_time(Ti),
	%findall(p(LP,Tempo),permuta_tempo_ocup(M,LP,Tempo), LL),
	findall(p(LP,Tatr),permuta_tempo_atraso(M,LP,Tatr), LA),
	%melhor_permuta(LL,Lm,Tm),
	melhor_permuta(LA,La,Ta),
	get_time(Tf),Tcomp is Tf-Ti,
	write('GERADO EM '),write(Tcomp),
	write(' SEGUNDOS'),nl.

mef(M, Lm, Tm) :-
	get_time(Ti),
	findall(p(LP, Tempo), permuta_tempo_atraso(M, LP, Tempo), LL),
	melhor_permuta(LL, Lm, Tm),
	get_time(Tf),
	Tcomp is Tf-Ti,
	write('GERADO EM '),write(Tcomp),
	write(' SEGUNDOS'),nl.

% 3 - Heurística para Somatório dos Tempos de Atraso

% h_soma_tempos_atraso([t(cla,pA,1,100), t(cla,pB,1,80)], Seq, Tempo).

h_soma_tempos_atraso(LEncomendas,Seq,Tempo) :-retractall(op_prod_cliente(_,_,_,_,_,_,_,_,_)),
cria_ops(LEncomendas,0),
heuristica_menor_tatraso(ma,Seq,Tempo),
retractall(op_prod_cliente(_,_,_,_,_,_,_,_,_)).


heuristica_menor_tatraso(M,Finallist,Tempo):-
	get_time(Ti),
	operacoes_atrib_maq(M,ListaO),
	lista_operacoes_tempo(ListaO,_,ListaOpTempoNaoOrdenada),
	ordenar_tempos_conclusao(ListaOpTempoNaoOrdenada,ListaOpTempo),
	obter_apenas_Ops(ListaOpTempo,_,Finallist),
	soma_tempos2(M,Finallist,Tempo),
	get_time(Tf),Tcomp is Tf-Ti,
	write('GERADO EM '),write(Tcomp),
	write(' SEGUNDOS'),nl.!.

compara_dupla('<', [_,A1],[_,A2]):-A1<A2,!.
compara_dupla('>',_,_).

ordenar_tempos_conclusao(Unsorted, Sorted):-
	predsort(compara_dupla,Unsorted,Sorted).

lista_operacoes_tempo([],ListaT,ListaTempoOps):- append(ListaT, [],ListaTempoOps).
lista_operacoes_tempo([HeadOp|List],ListaT,ListaTempoOps):-
	op_prod_client(HeadOp,_,_,_,_,_,TConc,_,_),
	lista_operacoes_tempo(List,[[HeadOp,TConc]|ListaT],ListaTempoOps).

obter_apenas_Ops([],ResultTmp,Result):-reverse(ResultTmp,ResultInverted),append(ResultInverted,[],Result),!.
obter_apenas_Ops([[HeadOp|_]|List],ResultTmp,Result):-obter_apenas_Ops(List,[HeadOp|ResultTmp],Result).

%Soma dos TemosAtraso
soma_tempos2(Machine,[HeadOp|ListOp],Result):-
	op_prod_client(HeadOp,Machine,Fer,_,_,_,TConc,Tsetup,Texec),
	Tempo is Tsetup + Texec,
	(	(Tempo-TCon)>0,!,TempoTmp is Tempo-TConc;TempoTmp is 0),!,
	soma_tempos2(Fer,Machine,ListOp,Tempo,TempoTmp,Result).

soma_tempos2(_,_,[],_,Result,Result).
soma_tempos2(Fer,Machine,[HeadOp|ListOp],Tempo,TempoTmp,Result):-!,
	op_prod_client(HeadOp,Machine,Fer2,_,_,_,TConc,Tsetup,Texec),
	(	(Fer==Fer2,!,TempoTmp2 is Texec+Tempo);
			TempoTmp2 is Tsetup+Texec+Tempo),
	(	(TempoTmp2-TConc)>0,!,TempoA1 is TempoTmp2-TConc+TempoTmp;
			TempoA1 is TempoTmp),

	soma_tempos2(Fer2,Machine,ListOp,TempoTmp2,TempoA1,Result).

% 4 - 

% 5 - 

% . . .


cria_op_enc:-retractall(operacoes(_)),
retractall(operacoes_atrib_maq(_,_)),retractall(classif_operacoes(_,_)),
retractall(op_prod_client(_,_,_,_,_,_,_,_,_)),
		findall(t(Cliente,Prod,Qt,TConc),
		(encomenda(Cliente,LE),member(e(Prod,Qt,TConc),LE)),
		LT),cria_ops(LT,0),
findall(Op,classif_operacoes(Op,_),LOp),asserta(operacoes(LOp)),
maquinas(LM),
 findall(_,
		(member(M,LM),
		 findall(Opx,op_prod_client(Opx,M,_,_,_,_,_,_,_),LOpx),
		 assertz(operacoes_atrib_maq(M,LOpx))),_).

cria_ops([],_).
cria_ops([t(Cliente,Prod,Qt,TConc)|LT],N):-
			operacoes_produto(Prod,LOpt),
	cria_ops_prod_cliente(LOpt,Prod,Cliente,Qt,TConc,N,N1),
			cria_ops(LT,N1).


cria_ops_prod_cliente([],_,_,_,_,Nf,Nf).
cria_ops_prod_cliente([Opt|LOpt],Client,Prod,Qt,TConc,N,Nf):-
		cria_ops_prod_cliente2(Opt,Prod,Client,Qt,TConc,N,Ni),
	cria_ops_prod_cliente(LOpt,Client,Prod,Qt,TConc,Ni,Nf).


cria_ops_prod_cliente2(_,_,_,0,_,Ni,Ni):-!.
cria_ops_prod_cliente2(Opt,Prod,Client,Qt,TConc,N,Ni2):-
			Ni is N+1,
			atomic_concat(op,Ni,Op),
			assertz(classif_operacoes(Op,Opt)),
			operacao_maquina(Opt,M,F,Tsetup,Texec),
	assertz(op_prod_client(Op,M,F,Prod,Client,Qt,TConc,Tsetup,Texec)),
	Qt2 is Qt -1,
	cria_ops_prod_cliente2(Opt,Prod,Client,Qt2,TConc,Ni,Ni2).




:-cria_op_enc.

