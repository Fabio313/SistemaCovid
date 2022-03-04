# SistemaCovid

obs:
-O sistema nao esta com tratamentos de erro(caso digite string em int ir quebra o programa);<br/>
-Quando se inicia o sistema : <br/>
  -caso seja um novo dia ira manter nos arquivos apenas quem esta em emergencia, lista de emergencia e o historico de pessoas com alta(nao esta apagando o resto dos arquivos<br/>
por isso nao importe quantos dias passe caso escolha opção de mesmo dia ira receber as pessoas do primeiro dia);
  -caso nao ira voltar com todas as lista inclusive filas (exceto a fila de pessoas na lista de espera para cadastro(necessiata utilizar o menu 9));
-Não fiz o medico alterar a situação do paciente;
-Durante o cadastro caso ja exista, não mostra o historico da pessoa;

  
Instruções: 
-Logo apos o programa iniciar digite o numero de leitos que o hospital possui no total;
-Escolha caso seja um novo dia ou caso esteja abrindo o programa no mesmo dia
-No menu a opção 1(next number) ira chamar a pessoa na lista de espera para a recepção,para que isso ocorra precisa-se usar a opção 9(New person arrives) para simular a
entrada de uma nova pessoa no hospital antes de de chamar a senha dela pela opção 1
(obs: caso queira ver a lista de pessoas na espera utilize a opção 5(Show Reception list(tickets)));
-Durante a opção 1, no cadastro da pessoa caso o cpf digitado ja esteja cadastrado o restante das informações sera inserido automaticamente, caso nao preencha
normalmente para um novo cadastro;
-Apos o cadastro a pessoa dependendo da data de nascimento sera redirecionada para a lista preferencial ou nao preferencial(visiveis pela opção 4(Show triage list) nessa opção
serao mostradas todas as pessoas esperando na lista para ir para a triagem(nao esta na ordem que serao chamadas));
-Para realizar a triagem da proximo pessoa da lista de espera use a opção 2(Next triage patient) que ira chamar o paciente por ordem de chegada com prioridade 2para1(2 idosos
para 1 nao idoso) e realizar a sua triagem;
-Após a triagem com base nas informações fornecidas o paciente ira ser redirecionado para a area designada(Sem covid, quarentena , emergencia ou espera para emergencia)
que podem ser vistas com as opções 8(Show no covid historic), 6(Show quarentine list), 3(Show Hospital beds pacients), e 7(Show Hospital beds list) respectivamente;
