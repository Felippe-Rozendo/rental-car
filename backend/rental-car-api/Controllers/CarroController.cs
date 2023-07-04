using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using rental_car_api.Contexts;
using rental_car_api.Contexts.DTO;
using rental_car_api.Enum;
using System.Globalization;

namespace rental_car_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {

        private readonly Context _db;

        public CarroController(Context db)
        {
            _db = db;
        }

        [HttpPost("obter-carros")]
        public async Task<IActionResult> ObterCarrosAsync(CancellationToken ct, [FromBody] FiltroCarroRequest filtro)
        {
            try
            {
                var query = await _db.Carros.AsNoTracking()
                                            .Where(f =>
                                                (string.IsNullOrEmpty(filtro.Marca) || f.Marca == filtro.Marca) &&
                                                (string.IsNullOrEmpty(filtro.Modelo) || f.Modelo == filtro.Modelo) &&
                                                (string.IsNullOrEmpty(filtro.PotenciaMin.ToString()) || f.Potencia >= filtro.PotenciaMin) &&
                                                (string.IsNullOrEmpty(filtro.PotenciaMax.ToString()) || f.Potencia <= filtro.PotenciaMax) &&
                                                (string.IsNullOrEmpty(filtro.TorqueMin.ToString()) || f.Torque >= filtro.TorqueMin) &&
                                                (string.IsNullOrEmpty(filtro.TorqueMax.ToString()) || f.Torque <= filtro.TorqueMax) &&
                                                (string.IsNullOrEmpty(filtro.Combustivel) || f.Combustivel == filtro.Combustivel) &&
                                                (string.IsNullOrEmpty(filtro.PrecoDiariaMin) || float.Parse(f.PrecoDiaria, CultureInfo.GetCultureInfo("en-US")) >= float.Parse(filtro.PrecoDiariaMin, CultureInfo.GetCultureInfo("en-US"))) &&
                                                (string.IsNullOrEmpty(filtro.PrecoDiariaMax) || float.Parse(f.PrecoDiaria, CultureInfo.GetCultureInfo("en-US")) <= float.Parse(filtro.PrecoDiariaMax, CultureInfo.GetCultureInfo("en-US"))) &&
                                                (string.IsNullOrEmpty(filtro.AnoMin.ToString()) || f.Ano >= filtro.AnoMin) &&
                                                (string.IsNullOrEmpty(filtro.AnoMax.ToString()) || f.Ano <= filtro.AnoMax))
                                            .Select(x => new CarroModel
                                            {
                                                Id = x.Id,
                                                Ano = x.Ano,
                                                Combustivel = EnumHelper.GetEnumDescription<CombustivelEnum>(x.Combustivel),
                                                Fotos = x.Fotos,
                                                Marca = x.Marca,
                                                Modelo = x.Modelo,
                                                Potencia = x.Potencia,
                                                PrecoDiaria = x.PrecoDiaria,
                                                //Reservas = x.Reservas,
                                                Torque = x.Torque
                                            }).ToListAsync(ct);

                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("criar-carro")]
        public async Task<IActionResult> CriarCarroAsync(CancellationToken ct, [FromBody] CarroDtoRequest carroModel)
        {
            try
            {
                var carro = new CarroModel
                {
                    Marca = carroModel.Marca,
                    Modelo = carroModel.Modelo,
                    Potencia = carroModel.Potencia,
                    Torque = carroModel.Torque,
                    Combustivel = carroModel.Combustivel,
                    PrecoDiaria = carroModel.PrecoDiaria,
                    Ano = carroModel.Ano
                };

                await _db.Carros.AddAsync(carro, ct);
                await _db.SaveChangesAsync(ct);

                return Ok(carro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("editar-carro")]
        public async Task<IActionResult> EditarCarroAsync(CancellationToken ct, [FromBody] CarroDtoRequest carroModel, int id)
        {
            try
            {
                var carro = await _db.Carros.FirstOrDefaultAsync(x => x.Id == id, ct);

                if (carro == null)
                    return BadRequest("Nenhum carro encontrado.");

                carro.Marca = carroModel.Marca;
                carro.Modelo = carroModel.Modelo;
                carro.Potencia = carroModel.Potencia;
                carro.Torque = carroModel.Torque;
                carro.Combustivel = carroModel.Combustivel;
                carro.PrecoDiaria = carroModel.PrecoDiaria;
                carro.Ano = carroModel.Ano;

                _db.Carros.Update(carro);
                await _db.SaveChangesAsync(ct);

                return Ok(carro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("excluir-carro")]
        public async Task<IActionResult> ExcluirCarroAsync(CancellationToken ct, int id)
        {
            try
            {
                var carro = await _db.Carros.FirstOrDefaultAsync(x => x.Id == id, ct);

                if (carro == null)
                    return BadRequest("Nenhum carro encontrado.");

                _db.Carros.Remove(carro);
                await _db.SaveChangesAsync(ct);

                return Ok("O Carro " + carro.Modelo + " foi excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
