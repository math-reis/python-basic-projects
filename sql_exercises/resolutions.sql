-- Comandos DML

-- SELECT
-- INSERT
-- UPDATE
-- DELETE

-- Sintaxe do comando SELECT

-- SELECT [* ou coluna ou expressão]
-- FROM [TABELA]
-- JOIN [TABELA]
-- ON [TABELA.COLUNA = TABELA.COLUNA]
-- WHERE [Condição]
-- GROUP BY [Agrupar valores iguais de uma coluna]
-- HAVING [Condição pós agrupamento]
-- ORDER BY [Ordem de apresentação]

-- Exercícios

-- Pg. 19

-- Exercício 1:
-- Selecione todas as informações disponíveis das regiões onde se localiza a 
-- empresa.

SELECT COD_REGIAO, NOME_REGIAO 
FROM REGIAO;

-- Exercício 2
-- Selecione o nome dos países cadastrados na empresa.

SELECT NOME_PAIS 
FROM PAIS;

-- Exercício 3
-- Selecione o nome das cidades cadastradas na empresa.

SELECT CIDADE 
FROM LOCAL;

-- Exercício 4
-- Selecione o nome do empregado e o seu percentual de comissão, apresentando a 
-- comissão com o cabeçalho "COMISSAO".

SELECT NOME, PCT_COMISSAO "COMISSAO" 
FROM EMPREGADO;

-- Pg. 25

-- Exercício 1
-- Mostrar o nome dos serviços e a diferença entre o salário máximo e o mínimo 
-- para cada um deles. O cabeçalho da coluna de diferenças deve ser “Dif”. 
-- Colocar a saída em ordem descendente das diferenças.

SELECT NOME_SERVICO, SALARIO_MAX - SALARIO_MIN "Dif"
FROM SERVICO
ORDER BY "Dif" DESC;

-- Exercício 2
-- Informe os diferentes COD_LOCAL onde há departamentos da empresa.

SELECT COD_LOCAL
FROM DEPARTAMENTO;

-- Exercício 3
-- Informe o código dos diferentes gerentes da empresa, em ordem descendente.

SELECT COD_GERENTE
FROM EMPREGADO
WHERE COD_GERENTE IS NOT NULL
ORDER BY COD_GERENTE DESC;

SELECT DISTINCT COD_GERENTE 
FROM DEPARTAMENTO
WHERE COD_GERENTE IS NOT NULL
ORDER BY COD_GERENTE DESC;

-- Exercício 4
-- Informe o nome e sobrenome de todos os empregados da empresa, por 
-- departamento, ordenando por departamento, descendente, e nome e sobrenome, 
-- ascendentes. Use a posição das colunas no resultado, para indicar o 
-- ordenamento.

SELECT NOME, SOBRENOME, COD_DEP
FROM EMPREGADO
ORDER BY 3 DESC, 1, 2;

-- Pg. 32

-- Exercício 1
-- Mostrar o COD_EMP e a DATA_INICIO do histórico dos empregados, onde 
-- COD_SERVICO for IT_PROG ou SA_MAN.

SELECT COD_EMP, DATA_INICIO
FROM HISTORIA_EMPREGADO
WHERE COD_SERVICO IN ('IT_PROG', 'SA_MAN');

-- Exercício 2
-- Informe o nome dos serviços cujo salário mínimo é menor que 4500 ou o 
-- salário máximo é maior que 12000.

SELECT NOME_SERVICO
FROM SERVICO
WHERE SALARIO_MIN < 4500 OR SALARIO_MAX > 12000;

-- Exercício 3
-- Informe o COD_LOCAL que não tem CEP.

SELECT COD_LOCAL
FROM LOCAL
WHERE CEP IS NULL;

-- Exercício 4
-- Mostre as cidades cujo nome comecem com letra a partir da letra S.

SELECT CIDADE
FROM LOCAL
WHERE CIDADE LIKE 'S%';

-- Pg. 38

-- Exercício 1
-- Mostre a frase 'Eu gosto de trabalhar com SQL', concatenando três grupos de 
-- caracteres diferentes.

SELECT 'Eu gosto ' || 'de trabalhar ' || 'com SQL'
FROM DUAL;

-- Exercício 2
-- Descubra em que posição ocorre a letra 'n' na palavra 
-- 'inconstitucionalissimamente', a partir da sétima posição. E a partir da 
-- posição 15?

SELECT INSTR('inconstitucionalissimamente', 'n', 7), 
       INSTR('inconstitucionalissimamente', 'n', 15)
FROM DUAL;

-- Exercício 3
-- Como fazer para adicionar 'tr' à esquerda de 'Sol' até completar 12 posições.
-- Observe a diferenciação do resultado se forem completadas 12 ou 11 posições.

SELECT LPAD('Sol', 12, 'tr')
FROM DUAL;

-- Exercício 4
-- Qual o comprimento do string resultante da operação de retirada de todos os 
-- brancos à esquerda de ' Teste ', sendo que há 3 brancos à esquerda e 3 à 
-- direita da palavra ?

SELECT LENGTH(LTRIM('   Teste   '))
FROM DUAL;

-- Pg. 48

-- Exercício 1
-- Faça com que a data '20110123 13:24:14' seja mostrada como 
-- '23/01/2011 13-24-14'.

SELECT TO_CHAR(TO_DATE('20110123 13:24:14', 'yyyymmdd hh24:mi:ss'), 
       'DD/MM/YYYY HH24-MI-SS')
FROM (SELECT TO_DATE('20110123 13:24:14', 'yyyymmdd hh24:mi:ss')
      FROM DUAL);

-- Exercício 2
-- Descobrir o último dia do mês atual.

SELECT LAST_DAY(SYSDATE)
FROM DUAL;

-- Exercício 3
-- Qual será o próximo sábado no formato DD/MM/YYYY.

SELECT NEXT_DAY(SYSDATE, 7)
FROM DUAL;

-- Exercício 4
-- Somar três dias a data atual e formatar no formato DD/MM/YYYY.

SELECT TO_CHAR(SYSDATE + 3, 'DD/MM/YYYY')
FROM DUAL;

-- Exercício 5
-- Descobrir quantos dias existem entre 01/02/2015 e 15/09/2015.

SELECT TO_DATE('15/09/2015', 'DD/MM/YYYY') - TO_DATE('01/02/2015', 'DD/MM/YYYY')
FROM DUAL;

-- Pg. 53

-- Exercício 1
-- Concatenar o nome e o sobrenome dos empregados do departamento 100 e mostrar
-- com o cabeçalho “Nome completo”.

SELECT NOME || ' ' || SOBRENOME "Nome completo"
FROM EMPREGADO;

-- Exercício 2
-- Mostrar o salário diário arredondado em duas casas decimais e o salário 
-- diário inteiro (sem casas decimais) para os empregados do departamento 30, 
-- ordenados pelo nome do empregado. Obs.: Salário Diário = dividir por 30.

SELECT ROUND(SALARIO / 30, 2), ROUND(SALARIO / 30)
FROM EMPREGADO
WHERE COD_DEP = 30
ORDER BY NOME;

-- Exercício 3
-- A partir do histórico dos empregados, informe o COD_EMP, o COD_SERVICO e o 
-- número de dias que o empregado ficou no serviço. Apresente o resultado para 
-- os departamentos 50 e 110. Mostre o número de dias como DIAS.

SELECT COD_EMP, COD_SERVICO, DATA_FIM - DATA_INICIO DIAS
FROM HISTORIA_EMPREGADO
WHERE COD_DEP IN (50, 110);

-- Pg. 60

-- Exercício 1
-- Mostrar a quantidade de empregados em cada departamento da empresa, ordenados
-- pelo código do departamento.

SELECT COUNT(COD_EMP)
FROM EMPREGADO;

-- Exercício 2
-- Mostrar a soma dos salários dos empregados da empresa, por COD_SERVICO, 
-- apenas quando o total de salários for maior do que 15.000, ordenados pelo 
-- total de salários, em ordem descendente. Mostrar o total como TOTAL.

SELECT COD_SERVICO, SUM(SALARIO) TOTAL
FROM EMPREGADO
GROUP BY COD_SERVICO
HAVING SUM(SALARIO) > 15000
ORDER BY TOTAL DESC;

-- Pg. 72

-- Exercício 1
-- Selecione o nome dos países localizados na região "Europe".

SELECT PAI.NOME_PAIS
FROM PAIS PAI
JOIN REGIAO REG
ON (PAI.COD_REGIAO = REG.COD_REGIAO)
WHERE REG.NOME_REGIAO = 'Europe';

-- Exercício 2
-- Selecione o nome do empregado e o nome do departamento para os empregados na 
-- cidade de Oxford.

SELECT EMP.NOME, DEP.NOME_DEP
FROM EMPREGADO EMP
JOIN DEPARTAMENTO DEP
ON (EMP.COD_DEP = DEP.COD_DEP)
JOIN LOCAL LOC
ON (DEP.COD_LOCAL = LOC.COD_LOCAL)
WHERE LOC.CIDADE = 'Oxford';

-- Exercício 3
-- Selecione o nome do empregado e o nome do gerente para os empregados que 
-- tenham salário maior do que 8000.

SELECT EMP.NOME, GER.NOME
FROM EMPREGADO EMP
JOIN EMPREGADO GER
ON (EMP.COD_GERENTE = GER.COD_EMP)
WHERE EMP.SALARIO > 8000;

-- Exercício 4
-- Informe o número de empregados cujo gerente é o Steven.

SELECT COUNT(EMP.NOME)
FROM EMPREGADO EMP
JOIN EMPREGADO GER
ON (EMP.COD_GERENTE = GER.COD_EMP)
WHERE GER.NOME = 'Steven';

-- Exercício 5
-- Mostre nome e sobrenome de todos os empregados que já tenham executado ou 
-- estejam executando o serviço de 'Sales Manager'.

SELECT EMP.NOME, EMP.SOBRENOME
FROM EMPREGADO EMP
JOIN SERVICO SER
ON (EMP.COD_SERVICO = SER.COD_SERVICO)
WHERE SER.NOME_SERVICO = 'Sales Manager'
UNION ALL
SELECT EMP.NOME, EMP.SOBRENOME
FROM HISTORIA_EMPREGADO HIS
JOIN EMPREGADO EMP 
ON (EMP.COD_EMP = HIS.COD_EMP)
JOIN SERVICO SER
ON (HIS.COD_SERVICO = SER.COD_SERVICO)
WHERE SER.NOME_SERVICO = 'Sales Manager';

-- Exercício 6
-- Mostre o nome dos departamentos e da cidade em que se localizam, para os 
-- departamentos localizados no Canada e em Germany (use UNION).

SELECT DEP.NOME_DEP, LOC.CIDADE
FROM DEPARTAMENTO DEP
JOIN LOCAL LOC
ON (DEP.COD_LOCAL = LOC.COD_LOCAL)
JOIN PAIS PAI
ON (LOC.COD_PAIS = PAI.COD_PAIS)
WHERE PAI.NOME_PAIS = 'Canada'
UNION
SELECT DEP.NOME_DEP, LOC.CIDADE
FROM DEPARTAMENTO DEP
JOIN LOCAL LOC
ON (DEP.COD_LOCAL = LOC.COD_LOCAL)
JOIN PAIS PAI
ON (LOC.COD_PAIS = PAI.COD_PAIS)
WHERE PAI.NOME_PAIS = 'Germany';
