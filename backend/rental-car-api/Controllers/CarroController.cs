using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rental_car_api.Contexts;
using rental_car_api.Contexts.DTO;
using rental_car_api.Enum;

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

        [HttpGet("obter-carros")]
        public async Task<IActionResult> ObterCarrosAsync(CancellationToken ct)
        {
            try
            {
                var query = await _db.Carros.AsNoTracking()
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
