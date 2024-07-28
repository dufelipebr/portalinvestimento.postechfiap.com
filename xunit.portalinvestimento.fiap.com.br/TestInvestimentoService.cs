using Bogus;
using Bogus.DataSets;
using Moq;
using portalinvestimento.virtualtilab.com;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Services;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.Xml;
using Xunit;
using portalinvestimento.virtualtilab.com.Entity;
using portalinvestimento.virtualtilab.com.Interfaces.Service;


namespace xunit.portalinvestimento.fiap.com.br
{
    /*
     * 
     * AssertionConcern.AssertArgumentNotEmpty(Codigo, "Codigo precisa ser preenchido.");
        AssertionConcern.AssertArgumentLength(Codigo, 50, "Codigo do Investimento precisa ter no maximo 10 caracteres.");
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
        AssertionConcern.AssertArgumentLength(Nome, 100, "Nome do Investimento precisa ter no maximo 50 caracteres.");
        AssertionConcern.AssertArgumentNotEquals(TipoInvestimento, 0, "Tipo Investimento precisa ser preenchido");
        AssertionConcern.AssertArgumentRange((double)TaxaADM, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
        AssertionConcern.AssertArgumentRange((double)AporteMinimo, 0.1, 1000000, "Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00");
     * 
     */
    public class TestInvestimentoService
    {
        private readonly Faker _faker;
        //private readonly portalinvestimento.virtualtilab.com.StringDictionary _sd;

        public TestInvestimentoService()
        {
            _faker = new Faker();
            //_sd = new portalinvestimento.virtualtilab.com.StringDictionary();
        }

        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Create_ShoulReturnSuccessMessage()
        {
            var mock = new Mock<IAplicacaoService>();
            mock.Setup(service => service.Create(It.IsAny<Ativo>())).Returns("Investimento ok!"); // codigo retorno succeso
            InvestimentoService s = new InvestimentoService();

            Ativo investimento = new Ativo(
                    Ativo.enTipoInvestimento.CDI,
                    "Simples Autom�tico RF",
                    "Fundo mais simples do bradesco destinado para o pov�o",
                    "Simples_Autom�tico_RF",
                    1.5m,
                    1.0m,
                    2.5m,
                    11.25m,
                    24.06m);
            //IInvestimentoService investimentoService = mockInvestimentoService.Object;
            //act 
            var resultaEsperado = mock.Object.Create(investimento);
            string resultado = s.Create(investimento);
                
            Assert.Equal(resultaEsperado, resultado);
        }

        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenCodigo_Empty()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = "";
            var taxaADM = 1.0m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Codigo precisa ser preenchido.", result.Message);

        }


        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenCodigo_Higher()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(51); ;
            var taxaADM = 1.0m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Codigo do Investimento precisa ter no maximo 50 caracteres.", result.Message);

        }


        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenNome_Empty()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = string.Empty;
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = 1.0m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Nome precisa ser preenchido.", result.Message);

        }


        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenNome_Higher()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(101);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = 1.0m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Nome do Investimento precisa ter no maximo 100 caracteres.", result.Message);

        }

        // N�o precisa de teste porque n�o � possivel setar um valor diferente Empty para o inteiro.
        //[Fact]
        //[Trait("Categoria", "Validando Investimento")]
        //public void Investimento_ShouldThrowException_WhenTipoInvestimento_Empty()
        //{
        //    // Arrange
        //    int tipo = 99; //Investimento.enTipoInvestimento.CDI;
        //    var nome = _faker.Random.String2(100);
        //    var descricao = _faker.Random.String2(500);
        //    var codigo = _faker.Random.String2(10);
        //    var taxaADM = 1.0m;
        //    var aporteMinimo = 10m;
        //    var rent_3 = 7.1m;
        //    var rent_12 = 20.3m;
        //    var rent_24 = 40.0m;

        //    //act
        //    var result = Assert.Throws<DomainException>(() => new Investimento((enTipoInvestimento)tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
        //    //Assert.Throws<DomainException>()

        //    //Assert
        //    Assert.Equal("Tipo Investimento precisa ser preenchido", result.Message);

        //}


        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenTaxaAdm_Lower()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = -0.1m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Taxa ADM precisa estar entre 0.1 e 10.", result.Message);

        }

        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenTaxaAdm_Higher()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = 10.5m;
            var aporteMinimo = 10m;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => new Ativo(tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Taxa ADM precisa estar entre 0.1 e 10.", result.Message);

        }

        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenAporteMinimo_Lower()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = 10m;
            var aporteMinimo = 0;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() => 
            new Ativo(
                tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00", result.Message);

        }


        [Fact]
        [Trait("Categoria", "Validando Investimento")]
        public void Investimento_ShouldThrowException_WhenAporteMinimo_Higher()
        {
            // Arrange
            var tipo = Ativo.enTipoInvestimento.CDI;
            var nome = _faker.Random.String2(100);
            var descricao = _faker.Random.String2(500);
            var codigo = _faker.Random.String2(10);
            var taxaADM = 10m;
            var aporteMinimo = 20000000000;
            var rent_3 = 7.1m;
            var rent_12 = 20.3m;
            var rent_24 = 40.0m;

            //act
            var result = Assert.Throws<DomainException>(() =>
            new Ativo(
                tipo, nome, descricao, codigo, taxaADM, aporteMinimo, rent_3, rent_12, rent_24));
            //Assert.Throws<DomainException>()

            //Assert
            Assert.Equal("Aporte Minimo precisa ser maior que 0 e menor que 1.000.00,00", result.Message);

        }











    }
}