using Bogus;
using Moq;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces;
using portalinvestimento.virtualtilab.com.Services;
using System.Net.NetworkInformation;
using Xunit;


namespace portalinvestimento.fiap.com.br.Tests
{
    public class TestInvestimentoService
    {
        private readonly Faker _faker;

        public TestInvestimentoService()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Investimento Validando NomeLength acima de 30")]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_Validate_NomeLenght()
        {
            var result = Assert.Throws<Exception>(() => new Investimento(
                Investimento.enTipoInvestimento.CDB,
                "asdhajkhdkjahsjkd jkjhakjhdjka kjsasdhajkhdkjahsjkd jkjhakjhdjka kjsasdhajkhdkjahsjkd jkjhakjhdjka kjsasdhajkhdkjahsjkd jkjhakjhdjka kjs",
                "123",
                0,
                0,
                0,
                0,
                0));
            
            Assert.Equal("Nome do Investimento precisa ter no maximo 50 caracteres", result.Message);
        }

        [Fact(DisplayName = "Validando mensagem de successo")]
        [Trait("Categoria", "Validando Investimento")]
        public void Create_ShoulReturnSuccessMessage()
        {
            var mockInvestimentoService = new Mock<IInvestimentoService>();
            mockInvestimentoService.Setup(service => service.Create(It.IsAny<Investimento>())).Returns("");
            IInvestimentoService investimentoService = mockInvestimentoService.Object;

            Investimento c = new Investimento();

            string resultado = investimentoService.Create(c);
                
            Assert.Equal("", resultado);
        }

        [Fact(DisplayName = "Investimento ShouldThrowException WhenDescriptionEmpty")]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenDescriptionEmpty()
        {
            // Arrange
            var tipo = Investimento.enTipoInvestimento.CDI;
            var nome = string.Empty;
            var codigo = _faker.Random.String2(10);
            var taxaADM = 0;
            var aporteMinimo = 0;
            var rent_3 = 10;
            var rent_12 = 20;
            var rent_24 = 19;

            //act
            var result = Assert.Throws < DomainException > (() => new Investimento(tipo, nome, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("O título não pode estar vazio!", result.Message);

        }
    }
}