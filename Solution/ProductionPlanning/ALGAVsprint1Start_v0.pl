:- dynamic operacao_tempo_atraso/3
% FÁBRICA

% Linhas

linhas([lA]).


% Máquinas


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


% Afetaãoo de tipos de operaçoes a tipos de maquinas
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


% permuta/2 gera permutações de listas
permuta([ ],[ ]).
permuta(L,[X|L1]):-apaga1(X,L,Li),permuta(Li,L1).

apaga1(X,[X|L],L).
apaga1(X,[Y|L],[Y|L1]):-apaga1(X,L,L1).

% permuta_tempo/3 faz uma permutação das operações atribu�das a uma maquina e calcula tempo de ocupação incluindo trocas de ferramentas

permuta_tempo(M,LP,Tempo):- operacoes_atrib_maq(M,L),
permuta(L,LP),soma_tempos(semfer,M,LP,Tempo).


soma_tempos(_,_,[],0).
soma_tempos(Fer,M,[Op|LOp],Tempo):- classif_operacoes(Op,Opt),
	operacao_maquina(Opt,M,Fer1,Tsetup,Texec),
	soma_tempos(Fer1,M,LOp,Tempo1),
	((Fer1==Fer,!,Tempo is Texec+Tempo1);
			Tempo is Tsetup+Texec+Tempo1),
			write('FERRAMENTA1 '),write(Fer1), write(' FERRAMENTA '),write(Fer),
	write(' Operation '),write(Op),
	write(' TempoAcumu '), write(Tempo),nl.

% melhor escalonamento com findall, gera todas as solucoes e escolhe melhor
melhor_escalonamento(M,Lm,Tm):-
				get_time(Ti),
				findall(p(LP,Tempo), 
				permuta_tempo(M,LP,Tempo), LL),
				melhor_permuta(LL,Lm,Tm),
				get_time(Tf),Tcomp is Tf-Ti,
				write('GERADO EM '),write(Tcomp),
				write(' SEGUNDOS'),nl.

melhor_permuta([p(LP,Tempo)],LP,Tempo):-!.
melhor_permuta([p(LP,Tempo)|LL],LPm,Tm):- melhor_permuta(LL,LP1,T1),
		((Tempo<T1,!,Tm is Tempo,LPm=LP);(Tm is T1,LPm=LP1)).


%testes
op_prod_cliente(op1,ma,fa,Prod,Cliente,Qt,100,5,60).
op_prod_cliente(op2,ma,fb,Prod,Cliente,Qt,50,6,30).
op_prod_cliente(op3,ma,fa,Prod,Cliente,Qt,50,5,60).

%Soma tempos Atraso
soma_tempos_atraso(_,_,[],0,0).
soma_tempos_atraso(Fer,M,[Op|LOp],Tempo,TempoAtraso):-
	op_prod_cliente(Op,M,Fer1,_,_,_,TConc,Tsetup,Texec),
	soma_tempos_atraso(Fer1,M,LOp,Tempo1, TempoAtraso1),
	((Fer1==Fer,!,Tempo is Texec+Tempo1);
			Tempo is Tsetup+Texec+Tempo1), ((Tempo > TConc,!,TempoAtraso is ((Tempo - TConc) + TempoAtraso1));TempoAtraso is TempoAtraso1),
	write('FERRAMENTA1 '),write(Fer1), write(' FERRAMENTA '),write(Fer),
	write(' Operation '),write(Op),write(' AtrasoAcum '), write(TempoAtraso),
	write(' TempoAcumu '), write(Tempo),nl.

soma_tempos_atraso_2(_,_,[],Tempo,TempoAtraso).
soma_tempos_atraso_2(Fer,M,[Op|LOp],Tempo,TempoAtraso):-
	op_prod_cliente(Op,M,Fer1,_,_,_,TConc,Tsetup,Texec),
	((var(Tempo),!, Tempo is 0, TempoAtraso is 0);true),
	((Fer1==Fer, !, Tempo1 is Tempo+Texec);Tempo1 is Tempo+Tsetup+Texec),
	write(Tempo1),nl,
	soma_tempos_atraso_2(Fer1,M,LOp,Tempo1,TempoAtraso).

soma_tempos_atraso_3(_,_,[],Tempo,TempoAtraso):-Tempo is 0,TempoAtraso is 0,!.
soma_tempos_atraso_3(Fer,M,[Op|LOp],Tempo,TempoAtraso):-
	op_prod_cliente(Op,M,FerAtual,_,_,_,TConc,Tsetup,Texec),
	((FerAtual==Fer, !, TempoAcumulado is Texec);TempoAcumulado is Tsetup+Texec), 
	((TempoAcumulado > TConc,!,TempoAtrasoAcumulado is (TempoAcumulado - TConc));TempoAtrasoAcumulado is 0),
	soma_tempos_atraso_3(FerAtual,M,LOp,TempoTMP,TempoAtrasoTMP),
	Tempo is TempoTMP + TempoAcumulado,
	TempoAtraso is TempoAtrasoTMP + TempoAtrasoAcumulado,
	%assertz
	assertz(operacao_tempo_atraso(Op,Tempo,TempoAtraso)).


	


%cria ops
cria_ops([],_).
cria_ops([t(Cliente,Prod,Qt,TConc)|LT],N):- operacoes_produto(Prod,LOpt),
	cria_ops_prod_cliente(LOpt,Prod,Cliente,Qt,TConc,N,Nf),
	cria_ops(LT,N1).		

cria_ops_prod_cliente([],_,_,_,_,Nf,Nf).
cria_ops_prod_cliente([Opt|LOpt],Cliente,Prod,Qt,TConc,N,Nf):-
	Ni is N+1,
	atomic_concat(op,Ni,Op),
	assertz(classif_operacoes(Op,Opt)),
	operacoes_maquina(Opt,M,F,Tsetup,Texec),
	assertz(op_prod_cliente(Op,M,F,Prod,Cliente,Qt,TConc,Tsetup,Texec)),
	cria_ops_prod_cliente(LOpt,Cliente,Prod,Qt,TConc,Ni,Nf).
			




