using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using src.Dto;
using src;
using Microsoft.Extensions.Configuration;
using AutoMapper;
#nullable enable
namespace src.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IConfiguration _configuration;
        private IMapper _mapper;
        public WeatherController(IConfiguration configuration, IMapper mapper) {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<string> GetWeather(float latitude, float longitude)

        {
            string weatherApiKey = _configuration.GetValue<string>("WeatherApiKey");
            string baseUrl = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={weatherApiKey}";
            using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    using(HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if(data != null)
                {
                    WeatherDto weatherDto = new WeatherDto();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    weatherDto.Temp = json["main"]["temp"];
                    weatherDto.Pressure = json["main"]["pressure"];
                    weatherDto.Humidity = json["main"]["humidity"];
                    weatherDto.Visibility = json["visibility"];
                    weatherDto.WindSpeed = json["wind"]["speed"];
                    weatherDto.WindDegree = json["wind"]["deg"];
                    weatherDto.Cloud = json["clouds"]["all"];
                    return JsonConvert.SerializeObject(weatherDto);
                }
                else
                {
                    return "error";
                }
            }
                        
        }
    }
}