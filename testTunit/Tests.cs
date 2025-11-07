using Bunit.Extensions;
using testTunit.Data;
namespace testTunit;

using Bunit;
using BlazorApp1.Components.Pages;

public class Tests
{  
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

    [Test]
    public async Task CalculaPuntosyCostoCorrectamente()
    {
        using var ctx = new Bunit.TestContext();
        var componente = ctx.RenderComponent<SoftwareCostCalculator>();

        // Completa los valores
        componente.Find("#ei").Change("2");
        componente.Find("#eo").Change("1");
        componente.Find("#eq").Change("0");
        componente.Find("#ilf").Change("0");
        componente.Find("#eif").Change("0");

        // Calcula puntos
        componente.Find("button.btn-secondary").Click();
        var mensaje1 = componente.FindAll("p[role='status']").First();
        await Assert.That(mensaje1.TextContent).Contains("Puntos de complejidad estimados");

        // Establece costo por punto y calcula costo total
        componente.Find("#cost").Change("100");
        componente.Find("button.btn-primary").Click();
        var mensaje2 = componente.FindAll("p[role='status']").Last();
        await Assert.That(mensaje2.TextContent).Contains("Costo total estimado");
    }

    [Test]
    public async Task CalculaCostoyTiempoCorrectamente()
    {
        using var ctx = new Bunit.TestContext();
        var componente = ctx.RenderComponent<SoftwareCostCalculator>();

        // Complejidad
        componente.Find("#ei").Change("2");
        componente.Find("#eo").Change("1");
        componente.Find("button.btn-secondary").Click();

        var mensajePuntos = componente.FindAll("p[role='status']").First();
        await Assert.That(mensajePuntos.TextContent).Contains("Puntos de complejidad");

        // Costo y tiempo
        componente.Find("#cost").Change("100");
        componente.Find("#hours").Change("4");
        componente.Find("button.btn-primary").Click();

        var mensajeCosto = componente.FindAll("p[role='status']").Last();
        await Assert.That(mensajeCosto.TextContent).Contains("Costo total estimado").And.Contains("horas").And.Contains("días");

    }

}
