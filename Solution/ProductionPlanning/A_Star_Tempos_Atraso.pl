%classif_operacoes(Operacao,TipoOperacao)
:- dynamic classif_operacoes/2.
%op_prod_cliente(Operacao,Maquina,Ferramenta,Produto,Cliente,Quantidade,TempoConclusao,TempoSetup,TempoExecuçao)
:- dynamic op_prod_cliente/9.
%ferramenta_inicial(Ferramenta)
:- dynamic ferramenta_inicial/1.

% Afetação de tipos de operaçoes a tipos de maquinas
% com ferramentas, tempos de setup e tempos de execucao)
operacao_maquina(opt1,ma,fa,5,60).
operacao_maquina(opt2,ma,fb,6,30).
operacao_maquina(opt3,ma,fc,8,40).
operacao_maquina(opt4,ma,fd,3,55).

% Produtos
operacoes_produto(pA,[opt1]).
operacoes_produto(pB,[opt2]).
operacoes_produto(pC,[opt3]).

%Testes
%Teste1 ---> %a_Star_Tempos_Atraso([t(clA,pA,1,100),t(clA,pB,1,100),t(clA,pA,1,110),t(clA,pB,1,150),t(clA,pC,1,300)],Seq,Custo,fd).

a_Star_Tempos_Atraso(LEncomendas,Seq,Custo,FerramentaInicial):-
cria_ops(LEncomendas,0,NOperacoes),
lista_operacoes(NOperacoes,LOperacoes),
assertz(ferramenta_inicial(FerramentaInicial)),
aStar(LOperacoes,Seq,Custo),retractall(op_prod_cliente(_,_,_,_,_,_,_,_,_)),retractall(ferramenta_inicial(_)).


lista_operacoes(0,[]) :-!.
lista_operacoes(N,L) :- N1 is N - 1,atomic_concat(op,N,Op),lista_operacoes(N1,L1),append(L1,[Op],L2),L = L2.

%recebe como parâmetro [t(Cliente,Produto,Quantidade,TempoConclusao),t(....),t(....)]
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
aStar2([(_,0,[],ListaOperacoes,0,FerramentaInicial)],Seq,Custo),!.

aStar2([(_,Custo,Result,[],_,_)|_],Seq,Custo):- reverse(Result,Seq).

aStar2([(_,CustoAcumuladoAtraso,LA,LOPFaltam,TempoAtual,FerramentaActual)|Outros],Seq,Custo):- 
            findall((CEAtraso,NovoCaAtraso,[X|LA],LOPFaltamNova,TaAtual,NovaFerramentaActual),
            (member(X,LOPFaltam),
            op_prod_cliente(X,_,NovaFerramentaActual,_,_,_,TConc,Tsetup,Texec),
            delete(LOPFaltam,X,LOPFaltamNova),
            %Custos
            calcula_valores(NovaFerramentaActual,CustoAcumuladoAtraso,NovoCaAtraso,TaAtual,TempoAtual,Tsetup,Texec,TConc,FerramentaActual), 
            %calcular estimativa
            estimativa(LOPFaltamNova,TempoAtual,EstAtraso),
            CEAtraso is  EstAtraso)
            ,Novos),
    append(Outros,Novos,Todos),
    sort(1, @=<,Todos,TodosOrd),
    aStar2(TodosOrd,Seq,Custo).

calcula_valores(NovaFerramentaActual,CustoAcumuladoAtraso,NovoCaAtraso,TaAtual,TempoAtual,Tsetup,Texec,TConc,FerramentaActual):-
%verifica se a ferramenta anterior esta em uso e calcula custo dependendo dissos
(NovaFerramentaActual = FerramentaActual,!,
        (%Custo de atraso
        CustoAtraso is (Texec + TempoAtual) - TConc,
        %Tempo acumulado atual
        TaAtual is TempoAtual + Texec)
        ;
        (%Custo de atraso
        CustoAtraso is (Texec + Tsetup + TempoAtual) - TConc,
        %Tempo acumulado atual
        TaAtual is TempoAtual + Tsetup + Texec)),
        %Custo acumulado de atraso    
        ((CustoAtraso > 0,!,NovoCaAtraso is CustoAcumuladoAtraso + CustoAtraso);(NovoCaAtraso is CustoAcumuladoAtraso)).

estimativa([],TaAtual,-TaAtual).
estimativa(LOp,TAtual,Estimativa) :- 
findall(Resultado,(member(Op,LOp),op_prod_cliente(Op,_,_,_,_,_,TConc,_,Texec),Resultado is TAtual - TConc + Texec ),Atrasos),
sort(0,  @>, Atrasos,  S),
calc_estimativa(S,Estimativa).

calc_estimativa([H|_],H).
