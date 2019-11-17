%classif_operacoes(Operacao,TipoOperacao)
:- dynamic classif_operacoes/2.
%op_prod_cliente(Operacao,Maquina,Ferramenta,Produto,Cliente,Quantidade,TempoConclusao,TempoSetup,TempoExecuçao)
:- dynamic op_prod_cliente/9.
%ferramenta_inicial(Ferramenta)
:- dynamic ferramenta_inicial/1.

% Afetacão de tipos de operaçoes a tipos de maquinas
% com ferramentas, tempos de setup e tempos de execucao)
operacao_maquina(opt1,ma,fa,5,60).
operacao_maquina(opt2,ma,fb,6,30).
operacao_maquina(opt3,ma,fc,8,40).


% produtos
produtos([pA,pB,pC]).
operacoes_produto(pA,[opt1]).
operacoes_produto(pB,[opt2]).
operacoes_produto(pC,[opt3]).

%Testes
%Test1 ---> a_Star_Tempos_Ocupacao([t(clA,pA,1,100),t(clA,pB,1,100),t(clA,pA,1,100),t(clA,pB,1,100),t(clA,pC,1,100)],Seq,Custo,fc).
%Test2 ---> a_Star_Tempos_Ocupacao([t(clA,pB,1,100),t(clA,pB,1,100),t(clA,pC,1,100)],Seq,Custo,fc).

a_Star_Tempos_Ocupacao(LEncomendas,Seq,Custo,FerramentaInicial):-
cria_ops(LEncomendas,0,NOperacoes),
lista_operacoes(NOperacoes,LOperacoes),
assertz(ferramenta_inicial(FerramentaInicial)),
aStar(LOperacoes,Seq,Custo),retractall(op_prod_cliente(_,_,_,_,_,_,_,_,_)),retractall(ferramenta_inicial).


lista_operacoes(0,[]) :-!.
lista_operacoes(N,L) :- N1 is N - 1,atomic_concat(op,N,Op),lista_operacoes(N1,L1),append(L1,[Op],L2),L = L2.


%recebe como parâmetro [t(Cliente,Produto,Quantidade,TempoConclusao),t(....),t(....)],0,NFO
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
	assertz(op_prod_cliente(Op,M,F,Prod,Cliente,Qt,TConc,Tsetup,Texec)),
	cria_ops_prod_cliente(LOpt,Cliente,Prod,Qt,TConc,Ni,Nf).


aStar(ListaOperacoes,Seq,Custo):- 
%get ferramenta inicial
ferramenta_inicial(FerramentaInicial),
%começa processo
aStar2([(_,0,[],ListaOperacoes,FerramentaInicial)],Seq,Custo),!.

aStar2([(_,Custo,Result,[],_)|_],Seq,Custo):- reverse(Result,Seq).

aStar2([(_,CustoAcumulado,LA,LOPFaltam,FerramentaActual)|Outros],Seq,Custo):- 
            findall((CEX,CaX,[X|LA],NovaLOPFaltam,NewFerramentaAtual),
            (member(X,LOPFaltam),
            op_prod_cliente(X,_,NewFerramentaAtual,_,_,_,_,Tsetup,Texec),
            delete(LOPFaltam,X,NovaLOPFaltam),
            %Custos
            calcula_custos(CaX,NewFerramentaAtual,Tsetup,Texec,FerramentaActual,CustoAcumulado),
            %calcular estimativa
            estimativa(NovaLOPFaltam,EstX),
            CEX is CaX + EstX)
            ,Novos),
    append(Outros,Novos,Todos),
    sort(Todos,TodosOrd),
    aStar2(TodosOrd,Seq,Custo).


calcula_custos(CaX,NewFerramentaAtual,Tsetup,Texec,FerramentaActual,CustoAcumulado) :-
    CustoX is CustoAcumulado + Texec,
    ((FerramentaActual = NewFerramentaAtual,!,CaX is CustoX); (CaX is CustoX + Tsetup)).

estimativa(LOp,Estimativa):-
findall(p(FOp,Tsetup),(member(Op,LOp),op_prod_cliente(Op,_,FOp,_,_,_,_,Tsetup,_)),LFTsetup),
sort(LFTsetup,L),
soma_setups(L,Estimativa).

elimina_repetidos([],[]).
elimina_repetidos([X|L],L1):-member(X,L),!,elimina_repetidos(L,L1).
elimina_repetidos([X|L],[X|L1]):-elimina_repetidos(L,L1).

soma_setups([],0).
soma_setups([p(_,Tsetup)|[]],Tsetup):-!.
soma_setups([p(_,Tsetup)|L],Ttotal):- soma_setups(L,T1), Ttotal is Tsetup+T1.

