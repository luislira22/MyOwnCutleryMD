:- dynamic cria_ops_prod_cliente/9.
:- dynamic classif_operacoes/2.

operacao_maquina(opt1,ma,fa,5,60).
operacao_maquina(opt2,ma,fb,6,30).
operacao_maquina(opt3,ma,fc,8,40).

%operacoes_atrib_maq depois deve ser criado dinamicamente
operacoes_atrib_maq(ma,[op1,op2,op3,op4,op5]).

% Produtos
produtos([pA,pB,pC]).

operacoes_produto(pA,[opt1]).
operacoes_produto(pB,[opt2]).
operacoes_produto(pC,[opt3]).

% 3 - Heurística para Somatório dos Tempos de Atraso

% h_menor_tempo_atraso([t(clA,pA,1,100),t(clA,pB,1,100),t(clA,pA,1,110),t(clA,pB,1,150),t(clA,pC,1,300)],Seq,Custo).

h_menor_tempo_atraso(LEncomendas,Seq,Tempo) :-
cria_ops(LEncomendas,0,NOperacoes),
write(NOperacoes),
heuristica_menor_tatraso(ma,Seq,Tempo),
retractall(op_prod_cliente(_,_,_,_,_,_,_,_,_)).


heuristica_menor_tatraso(M,Finallist,Tempo):-
	get_time(Ti),
	operacoes_atrib_maq(M,ListaO),
	%lista_operacoes_tempo(ListaO,_,ListaOpTempoNaoOrdenada),
    findall((Op,), Goal, Bag)
	%ordenar_tempos_conclusao(ListaOpTempoNaoOrdenada,ListaOpTempo),
	obter_apenas_Ops(ListaOpTempo,_,Finallist),
	soma_tempos2(M,Finallist,Tempo),
	get_time(Tf),Tcomp is Tf-Ti,
	write('GERADO EM '),write(Tcomp),
	write(' SEGUNDOS'),nl,!.

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
	((Tempo-TConc)>0,!,TempoTmp is Tempo-TConc;
	TempoTmp is 0),!,
	soma_tempos2(Fer,Machine,ListOp,Tempo,TempoTmp,Result).

soma_tempos2(_,_,[],_,Result,Result).
soma_tempos2(Fer,Machine,[HeadOp|ListOp],Tempo,TempoTmp,Result):-!,
	op_prod_client(HeadOp,Machine,Fer2,_,_,_,TConc,Tsetup,Texec),
	(	(Fer==Fer2,!,TempoTmp2 is Texec+Tempo);
			TempoTmp2 is Tsetup+Texec+Tempo),
	(	(TempoTmp2-TConc)>0,!,TempoA1 is TempoTmp2-TConc+TempoTmp;
			TempoA1 is TempoTmp),

	soma_tempos2(Fer2,Machine,ListOp,TempoTmp2,TempoA1,Result).

cria_ops([],NFO,NFO).
cria_ops([t(Cliente,Prod,Qt,TConc)|LT],N,NFO):- operacoes_produto(Prod,LOpt),
	cria_ops_prod_cliente(LOpt,Prod,Cliente,Qt,TConc,N,Nf),
	cria_ops(LT,Nf,NFO).		

cria_ops_prod_cliente([],_,_,_,_,Nf,Nf).
cria_ops_prod_cliente([Opt|LOpt],Cliente,Prod,Qt,TConc,N,Nf):-
	Ni is N+1,
	atomic_concat(op,Ni,Op),
	assertz(classif_operacoes(Op,Opt)),
	operacao_maquina(Opt,M,F,Tsetup,Texec),
	assertz(op_prod_client(Op,M,F,Prod,Cliente,Qt,TConc,Tsetup,Texec)),
	cria_ops_prod_cliente(LOpt,Cliente,Prod,Qt,TConc,Ni,Nf).	