using Microsoft.AspNetCore.Mvc;

namespace ProgWebIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public List<WeatherForecast> tempos { get; set; } 
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            tempos = Enumerable.Range(1, 5).Select(diasAmais => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(diasAmais),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }
        //https://localhost:7228/WeatherForecast GET
        [HttpGet]
        public IEnumerable<WeatherForecast> Consulta()
        {
            return tempos;
        }

        //https://localhost:7228/WeatherForecast POST
        [HttpPost]
        public WeatherForecast Insert(WeatherForecast tempo)
        {
            tempos.Add(tempo);
            return tempo;
        }

        //https://localhost:7228/WeatherForecast PUT
        [HttpPut]
        public WeatherForecast Atualizar(int quemQuerAtualizar, WeatherForecast oQueQuerAtualizar)
        {
            tempos[quemQuerAtualizar] = oQueQuerAtualizar;
            return tempos[quemQuerAtualizar];
        }

        //https://localhost:7228/WeatherForecast DELETE
        [HttpDelete]
        public List<WeatherForecast> Deletar(int index)
        {
            tempos.RemoveAt(index);
            return tempos;
        }
    }
}