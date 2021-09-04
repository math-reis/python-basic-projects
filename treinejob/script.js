const questions = [
  {
    question: "Você poderia falar um pouco sobre você?",
    example:
      "De forma sucinta e objetiva, relate as suas experiências pessoais e profissionais que sejam pertinentes para o cargo. Evite divagar sobre assuntos menos relevantes.",
  },
  {
    question: "Como você ficou sabendo da vaga?",
    example:
      "Caso tenha descoberto a vaga através de um anúncio ou site, conte o que te chamou atenção e por que você se interessou por ela. Caso tenha sido um indicação, mencione quem a indicou.",
  },
  {
    question: "O que você sabe sobre a nossa empresa?",
    example:
      "Tenha a certeza de estudar antecipadamente sobre a empresa. Conheça a sua missão, os seus objetivos e seus valores, e demonstre o quanto você está alinhado com os mesmos. Se for preciso, entre em contato com a própria empresa ou com seus funcionários para conhecê-la melhor.",
  },
  {
    question: "Por que você quer essa vaga?",
    example:
      "Demonstre conhecimento e interesse pela vaga e pela empresa, e deixe claro o porquê delas serem excelentes oportunidades para você.",
  },
  {
    question: "Por que devemos contratar você?",
    example:
      "Demonstre por que você é a pessoa certa para o cargo, e por que será um bom investimento para a empresa. Mostre que você não apenas pode fazer o trabalho proposto, mas que também o pode fazer de maneira excelente e alinhada com a sua cultura.",
  },
  {
    question: "Quais são os seus pontos fortes?",
    example:
      "Reflita e fale sobre os seus reais pontos fortes e relevantes para o cargo. Não menospreze as suas capacidades, mas também evite mencionar qualidades que você não possui.",
  },
  {
    question: "Quais são seus pontos fracos?",
    example:
      "Seja honesto e autoconsciente. Mencione os seus reais pontos fracos, e deixe claro que você pode e está se esforçando para melhorá-los. Evite exageros, tanto para mais quanto para menos.",
  },
  {
    question: "Qual é o seu emprego dos sonhos?",
    example:
      "Fale sobre as suas ambições e objetivos futuros, mas evite manifestar desinteresse pelo cargo atual. Mostre que, mesmo tendo outros sonhos, atualmente você está totalmente focado e alinhado com os valores e objetivos da empresa.",
  },
  {
    question: "Você está se candidatando a outras vagas?",
    example:
      "Seja sincero, mas evite demonstrar falta de foco e interesse pela empresa. Caso tenha se candidatado a outras vagas, identifique os pontos em comum entre elas, e deixe claro que você está explorando vagas do mesmo segmento.",
  },
  {
    question: "Por que você está deixando seu emprego atual?",
    example:
      "Evite falar de forma negativa sobre o seu atual ou antigo emprego. O melhor é demonstrar o seu desejo de evoluir, evidenciando que essa é uma boa oportunidade para isso. Deixe claro que a vaga pretendida é mais adequada para você do que a sua posição atual ou anterior.",
  },
  {
    question: "Por que você foi demitido?",
    example:
      "Não é necessário dar detalhes sobre a sua demissão, mas é preciso ser honesto na resposta. Se possível, identifique os pontos positivos que você obteve com a experiência, e demonstre que você está continuamente evoluindo com os seus erros.",
  },
  {
    question: "O que você procura em uma nova posição?",
    example:
      "Seja direto e cite exatamente os pontos que te interessam, pois o recrutador quer saber se você está alinhado com o que a vaga e a empresa têm para te oferecer.",
  },
  {
    question: "Que tipo de ambiente de trabalho você prefere?",
    example:
      "Certifique-se de pesquisar sobre a empresa e sobre o ambiente de trabalho ao qual está se candidatando, e alinhar a sua resposta a essa realidade.",
  },
  {
    question: "Qual o seu estilo de gestão?",
    example:
      "As empresas buscam gestores que sejam líderes e, ao mesmo tempo, flexíveis. Se você está concorrendo a uma vaga de gestão, certifique-se de fornecer exemplos de conquistas em suas atuações passadas.",
  },
  {
    question: "Você já exerceu liderança? Em quais situações?",
    example:
      "Formule sua resposta com base na vaga a qual está se candidatando. Escolha exemplos que demonstrem suas habilidades de gerenciamento e liderança, e que estejam alinhados aos objetivos do cargo e da empresa.",
  },
  {
    question: "Você já discordou de alguma decisão de trabalho?",
    example:
      "Se a resposta for positiva, busque exemplos de situações em que a discordância foi produtiva e saudável, e certifique-se de demonstrar que o seu intuito era de melhorar um problema real.",
  },
  {
    question: "O que o seu chefe e seus colegas falariam de você?",
    example:
      "Aproveite a oportunidade para realçar qualidades e características suas que não foram mencionadas durante a entrevista.",
  },
  {
    question: "Qual foi a sua maior conquista profissional?",
    example:
      "Apresente exemplos concretos de conquistas que demonstrem sua experiência e competência para o cargo. Contextualize a situação, descreva o que foi feito, e quais resultados foram obtidos.",
  },
  {
    question: "Por que existe uma lacuna no seu currículo?",
    example:
      "Deixe claro os motivos das lacunas, e evidencie outras atividades relevantes que você fez durante o período. Em todo caso, demonstre que está disposto a evoluir e retornar às atividades, e que está trabalhando duro para isso.",
  },
  {
    question: "Por que você está mudando de carreira?",
    example:
      "Explique os motivos que levaram você a migrar de carreira, os benefícios dessa mudança, e, se possível, como que as experiências passadas irão auxliar você nessa nova fase.",
  },
  {
    question: "Como você lida com situações estressantes e de pressão?",
    example:
      "Busque exemplos de situações estressantes que você teve que lidar no passado, e mostre como você fez para contorná-las ou superá-las sem que elas te afetassem.",
  },
  {
    question: "Você tem alguma pergunta?",
    example:
      "Utilize essa oportunidade para sanar dúvidas genuínas sobre o cargo ou sobre a empresa que não foram discutidas durante a entrevista. É uma boa oportunidade para demonstrar conhecimento e interesse sobre o local que você pretende trabalhar.",
  },
  {
    question: "Onde você se vê daqui a cinco anos?",
    example:
      "Seja honesto quanto às suas ambições e expectativas de carreira. Pense onde essa posição pode te levar, e de que forma isso se alinha com os seus objetivos de crescimento pessoal e profissional.",
  },
  {
    question:
      "Fale sobre um desafio ou conflito que você enfrentou no trabalho, e como você lidou com isso.",
    example:
      "Busque exemplos de situações reais que você já passou, e como você lidou para superá-las. Dê preferência a situações que demonstrem habilidades e capacidades semelhantes às requisitadas pelo cargo.",
  },
  {
    question: "Quais são as suas expectativas de salário?",
    example:
      "Pesquise anteriormente sobre o salário médio do cargo almejado. A busca pode ser feita em sites, em grupos relacionados ao cargo, ou até mesmo diretamente com funcionários da empresa. Aqui o recrutador simplesmente quer saber se vocês estão em consonância quanto a realidade salarial do cargo, evitando qualquer tipo de decepções futuras.",
  },
  {
    question: "Como você trabalha em equipe?",
    example:
      "Deixe claro que você gosta e possui traquejo para trabalhar em equipe. Não pode restar dúvida ao recrutador que você possui essa habilidade, pois empresas são essencialmente grupos de pessoas reunidas em prol de um objetivo comum. Trabalhar em equipe, portanto, é requisito essencial para a maioria dos cargos.",
  },
  {
    question: "Qual é a sua filosofia quanto ao trabalho?",
    example:
      "Dê vazão aos seus valores e cultura de trabalho, mas deixe claro que você está disposto a dar o seu melhor para que as tarefas sejam feitas, especialmente as urgentes e essenciais.",
  },
  {
    question: "O que te irrita mais nos seus colegas?",
    example:
      "Evite trazer características e atitudes pontuais que te irritam. Prefira uma resposta positiva e conciliatória, deixando claro que qualquer desavença pode e deve ser superada com equilíbrio, tolerância, paciência e profissionalismo.",
  },
  {
    question: "Qual seu maior atributo?",
    example:
      "Pense em características positivas e que estejam alinhadas com as expectativas da empresa. Esteja a par das habilidades e competências exigidas para o cargo, e tente apresentá-las como atributos que você possui ou almeja.",
  },
  {
    question: "Com que tipo de pessoa recusaria trabalhar?",
    example:
      "É importante deixar claro que você jamais se recusaria a trabalhar com alguém, a menos que essa pessoa exceda níveis extremos de práticas intoleráveis, como violência, corrupção, deslealdade, crimes e afins.",
  },
  {
    question: "O que é mais importante: dinheiro ou trabalho?",
    example:
      "Seja sincero quanto aos seus valores, mas evite respostas pouco equilibradas. A melhor saída é formular algo que vá em direção a ideia de que, embora o dinheiro proporcione sustento, o trabalho proporciona realização.",
  },
  {
    question: "O que motiva você a trabalhar melhor?",
    example:
      "Identifique os valores e objetivos que movem você a fazer um bom trabalho. Evite associar sua motivação a ganhos financeiros, e dê preferência aos ganhos pessoais.",
  },
  {
    question: "O que você aprendeu com os seus erros do passado?",
    example:
      "Foque em evidenciar o que você tirou de proveito dos seus erros, e não os erros em si.",
  },
  {
    question: "Que qualidades você admira em um superior?",
    example:
      "Enfatize qualidades que você julga serem essenciais para um bom chefe, como liderança, conhecimento, senso de humor oportuno, descontração com seriedade, entre outras.",
  },
  {
    question:
      "O que você fez para melhorar os seus conhecimentos técnicos no último ano?",
    example:
      "Mencione todas as atividades oportunas de desenvolvimento profissional e pessoal que você se engajou, dando preferência àquelas relevantes para o cargo.",
  },
  {
    question: "Você já teve que demitir alguém? Como foi?",
    example:
      "Evite parecer que você gostou ou não de demitir alguém. Tente mostrar que o profissionalismo foi essencial para essa situação.",
  },
  {
    question: "Já solicitaram que você abandonasse uma função?",
    example:
      "Se a resposta for positiva, seja honesto e breve, procurando deixar claro que você deu o melhor de si no cargo, e que infelizmente não houve uma identificação profunda com as funções a serem desempenhadas.",
  },
  {
    question:
      "Conte alguma sugestão que você apresentou no seu último cargo, e como que ela se concretizou.",
    example:
      "Dê exemplo de situações em que uma ideia sua foi aceita, implementada, e bem-sucedida. Caso isso não tenha acontecido, tente lembrar de ocasiões em que você acreditava ter uma boa ideia, mas que por motivos alheios não pode ser aceita ou implementada.",
  },
  {
    question: "Você já teve problemas com superiores? Conte como foi.",
    example:
      "Evite a todo custo falar negativamente de algum antigo chefe. Dê preferência a uma resposta ética, comentando apenas o necessário para tornar claro que havia divergências entre vocês. Se for o caso, enfatize que as divergências foram solucionadas pacificamente e com profissionalismo.",
  },
  {
    question: "Como você pretende compensar a sua falta de experiência?",
    example:
      "Demonstre, com exemplos de situações reais, que você possui empenho, esforço, capacidade, e vontade de aprender. Caso não tenha experiência para o cargo, é preciso mostrar que você está apto a superar as dificuldades iniciais para desempenhar a função.",
  },
  {
    question: "Qual foi o seu maior desapontamento profissional?",
    example:
      "Evite demonstrar raiva ou decepção. O melhor caminho é evidenciar que você aprendeu e evoluiu com as frustrações passadas.",
  },
  {
    question: "Você pretende trabalhar por quanto tempo com nós?",
    example:
      "Deixe claro que você pretende trabalhar para empresa enquanto estiver realizando um bom trabalho, mas que também anseia evoluir na carreira caso novas oportunidades surjam. Aqui a melhor abordagem é apostar no equilíbrio.",
  },
  {
    question:
      "Está disposto a trabalhar para além de seu horário, à noite ou fins de semana?",
    example:
      "Responda com sinceridade e moderação. Deixe claro que, havendo um prévio acordo, não há problemas em uma possível mudança de rotina, uma vez que é preciso manter o equilíbrio com os outros âmbitos da vida. Enfatize, também, que você está aberto a negociações e sugestões para suprir as necessidades da empresa. Caso não seja viável para você trabalhar em horários diversos aos pré-estabelecidos, deixe isso claro ao entrevistador.",
  },
  {
    question:
      "Você estaria disposto a mudar de local de trabalho caso fosse necessário?",
    example:
      "Evite responder por impulso para agradar o entrevistador. Deixe claro que, havendo um acordo prévio, é uma possibilidade a ser considerada. Seja claro e honesto com o entrevistador e consigo próprio, e analise se essa é de fato uma situação pela qual você estaria disposto a passar. Se você acha que essa pergunta poderá surgir em sua entrevista, converse antes com a sua família.",
  },
  {
    question:
      "Você está disposto a colocar os interesses da empresa acima dos seus próprios?",
    example:
      "Responda com moderação. Procure manter uma posição de equilíbrio, frisando que sempre estará disposto a fazer o que for necessário pela empresa, especialmente para o seu crescimento nela e com ela.",
  },
  {
    question: "Que hobbies você tem fora do trabalho?",
    example:
      "Procure enfatizar atividades que você participa em conjunto com outras pessoas, como esportes e atividades lazer, mostrando que você é uma pessoa que opera bem em equipe.",
  },
  {
    question: "Tem algo que você gostaria de fazer de diferente nesta empresa?",
    example:
      "Demonstre que você é uma pessoa com boas ideias. Evite, no entanto, desmerecer ou criticar aspectos da empresa que você, a partir de sua posição, não tem controle ou mérito para avaliar.",
  },
  {
    question: "Como você reage a críticas?",
    example:
      "Deixe claro que você é aberto a críticas, desde que construtivas, e que está sempre em busca do desenvolvimento pessoal e profissional. O foco da resposta deve estar nas ações tomadas após a crítica, e não sobre o modo como você reage a elas.",
  },
  {
    question: "Você não acha que tem qualificações demais para a vaga?",
    example:
      "A intenção principal dessa pergunta é a de avaliar o seu potencial de desinteresse pelo trabalho a longo prazo. Deixe claro que, apesar das suas qualificações, você está disposto e entusiasmado com essa nova oportunidade. Evite demonstrar que a vaga será apenas temporária.",
  },
  {
    question: "Qual foi o último livro que leu?",
    example:
      "Essa pergunta procura avaliar um pouco da personalidade do candidato através dos seus gostos, e também avaliar o seu interesse pela área de trabalho. Procure ter em mente sempre alguns bons livros para comentar sobre.",
  },
  {
    question: "Você poderia resumir o seu currículo?",
    example:
      "Seja direto e objetivo, e trace um panorama geral da sua formação e de suas experiências profissionais. Não é o caso de abordar aqui outros aspectos da sua vida pessoal.",
  },
  {
    question: "Fale um pouco sobre a sua formação acadêmica.",
    example:
      "Se for o caso, trace um panorama geral sobre a sua formação superior até o momento, e busque enfatizar as realizações e feitos mais importantes do período. ",
  },
  {
    question: "Podemos conversar em inglês?",
    example:
      "Se no seu currículo constar o conhecimento de inglês avançado, esteja preparado para uma entrevista bilíngue. Caso você não se sinta confiante e confortável com isso, evite colocar no currículo o nível avançado.",
  },
  {
    question: "Como você avalia sua inteligência emocional?",
    example:
      "Esteja ciente de suas capacidades afetivas e de sua maturidade emocional. Enfatize suas habilidade em lidar com frustrações, estresse, e outras situações no cotidiano de uma empresa.",
  },
  {
    question:
      "O que você faz para aprimorar os seus conhecimentos técnicos na área?",
    example:
      "O objetivo é saber se você se mantém atualizando, e se você encara conhecimento como um investimento em si próprio. Resgate as suas participações em cursos, congressos, seminários, suas leituras e seus projetos em desenvolvimento.",
  },
  {
    question: "Como você tem lidou com a pandemia?",
    example:
      "O recrutador quer verificar como foi a sua capacidade de adaptação à pandemia, e se você buscou autodesenvolvimento durante o período. Fale sobre cursos online, livros, esportes, e outras atividades que você começou ou manteve. Se você souber que a empresa adotou home office, deixe claro que você está adaptado a essa nova realidade de trabalho.",
  },
  {
    question: "Dica: nunca minta no currículo ou na entrevista!",
    example:
      "Por mais que você queira passar a imagem de uma pessoa mais qualificada, a atitude de mentir pode se tornar mais prejudicial do que a própria falta de qualificação. Além de ser fácil para o entrevistador perceber algumas inconsistências no seu currículo, esta atitude pode deixar você marcado para além das portas da empresa. Saiba que é possível valorizar suas habilidades e ser franco sobre suas falhas sem precisar recorrer a exageros ou histórias mal contadas.",
  },
  {
    question: "Dica: conheça o método S.T.A.R.",
    example:
      "Para perguntas comportamentais do tipo “me dê um exemplo de uma vez em que você soube ser flexível”, é sempre valioso utilizar o método “STAR” (situação, tarefa, ação, resultado). Situação: descreva um problema (situação) que precisava de solução, um projeto, um desafio qualquer. Tarefa: especifique os serviços necessários para a resolução do problema. Ação: deixe claro qual foi a sua contribuição efetiva para a resolução do problema. Resultado: apresente o que a empresa e/ou você ganharam com o ocorrido.",
  },
  {
    question: "Dica: pratique o autoconhecimento!",
    example:
      "Talvez a dica mais importante de todas, e talvez a mais negligenciada. O autoconhecimento é um exercício diário de reconhecimento dos seus pontos fortes, fracos, de suas capacidades, habilidades e experiências. É a partir do autoconhecimento que ganhamos confiança e firmeza em nossas respostas. Portanto, não busque respostas prontas, e sim respostas próprias. O entrevistador ouve diariamente respostas muito semelhantes umas às outras. Se você exercitar o seu autoconhecimento, com certeza realizará uma entrevista mais autentica e que irá marcar o entrevistado.",
  },
  {
    question: "Dica: pratique constantemente suas respostas!",
    example:
      "Fale consigo mesmo em voz alta. Treine suas falas. Pense nas perguntas e ouça suas respostas. Se preferir, treine com algum amigo ou familiar, ou até mesmo com a câmera do celular. Melhore suas respostas, encurte-as, busque falar com desenvoltura. Quanto mais confiança você tiver em suas palavras, menos nervosismo você vai sentir na hora da entrevista, e consequentemente melhor ela será.",
  },
  {
    question: "Dica: pesquise sobre a empresa!",
    example:
      "Use o Google, procure pessoas que já trabalharam na empresa, levante todas as informações possíveis. Mencionar algo da história ou dos números da empresa é um sinal de que você realmente está interessado na vaga.",
  },
  {
    question: "Dica: pesquise sobre a vaga!",
    example:
      "Tente descobrir detalhes sobre a vaga. Além de proporcionar mais segurança sobre o trabalho que você pretende desempenhar, essas informações lhe darão embasamento para perguntas pertinentes sobre na hora da entrevista.",
  },
  {
    question: "Dica: preste atenção ao seu corpo!",
    example:
      "Olhe nos olhos do entrevistador, tenha uma postura ereta na cadeira, e evite ficar de braços cruzados ou mexendo as pernas. A postura corporal pode ser um grande aliado, mas também um grande vilão, na hora da entrevista.",
  },
  {
    question: "Dica: calibre o seu tom de voz!",
    example:
      "Ficar alterando o tom de voz passa a ideia de nervosismo. Por outro lado, falar muito baixo indica um excesso de timidez. Muito alto, então, é descontrole – ou indício de que você pode ser um chefe autoritário. Busque sempre um tom netro e contínuo, porém confiante, em suas palavras.",
  },
];

const randomQuest = () => {
  let random = questions[Math.floor(Math.random() * questions.length)];
  document.getElementById("question").innerHTML = random.question;
  document.getElementById("example").innerHTML = random.example;
};
