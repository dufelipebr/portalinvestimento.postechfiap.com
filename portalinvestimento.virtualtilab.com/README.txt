Você deverá desenvolver uma aplicação em .NET que simule um sis-
tema de gestão de investimentos seguindo os princípios da Clean
Architecture e com foco em alta qualidade de software.
O desafio consiste em criar uma plataforma que permita que usuários
gerenciem seus portfólios de investimentos, incluindo ações, títulos e
criptomoedas. A aplicação deve ser desenvolvida de acordo com a
Clean Architecture para garantir uma boa separação de preocupações
e facilitar a manutenção e a escalabilidade do sistema.
Para te ajudar na etapa de modelagem, a seguir você tem algumas en-
tidades que podem ser utilizadas neste sistema:
Requisitos

Entidade:: Usuario
// Ter uma área logada.
// Ter uma forma de solicitar acesso.
// Id: Identificador único do usuário.
// Nome: Nome completo do usuário.
// Email: Email do usuário.
// Senha: Hash da senha para autenticação.

Entidade: Portfolio
// Id: Identificador único do portfólio.
// UsuarioId: Referência ao usuário proprietário do portfólio.
// Nome: Nome descritivo do portfólio.
// Descrição: Descrição detalhada do portfólio.

Entidade: Ativo
// Id: Identificador único do ativo.
// TipoAtivo: Tipo do ativo (e.g., Ações, Títulos, Criptomoedas).
// Nome: Nome do ativo.
// Codigo: Código de negociação do ativo (e.g., AAPL para Apple, BTC para Bitcoin).


Entidade: Transação
// Id: Identificador único da transação.
// PortfólioId: Referência ao portfólio em que a transação foi realizada.
// AtivoId: Referência ao ativo negociado.
// TipoTransacao: Tipo da transação (Compra ou Venda).
// Quantidade: Quantidade de ativos negociados.
// Preco: Preço por unidade do ativo no momento da transação.
// DataTransacao: Data e hora em que a transação foi efetuada.…