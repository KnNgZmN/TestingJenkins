using Bunit.Extensions;
using testTunit.Data;
namespace testTunit;
using Bunit;
using BlazorApp1.Components.Pages;

public class Tests
{
   /* [Test]
    public async Task Basic()
    {
        Console.WriteLine("This is a Basic Test");
        var Bunitctx = new Bunit.TestContext();
        var Componente = Bunitctx.RenderComponent<Counter>();
        Componente.Find("button").Click();
        var Resultado = Componente.Find("p[role='status']");
        await TUnit.Assertions.Assert.That(Resultado.TextContent).Contains("Current count: 1");
    }*/

    [Test]

        public async Task MuestraMensajeDeConfirmacion()
    {
        using var ctx = new Bunit.TestContext();
        var componente = ctx.RenderComponent<Contactenos>();        
        componente.Find("#nombre").Change("Kevin Guzmán");
        componente.Find("#email").Change("knngzmn@gmail.com");
        componente.Find("#mensaje").Change("Hola, quiero más información");
        componente.Find("button[type='submit']").Click();
        var resultado = componente.Find("p[role='status']");
        await Assert.That(resultado.TextContent).Contains("Gracias por contactarnos");
    }
}
