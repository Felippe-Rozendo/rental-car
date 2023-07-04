using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rental_car_api.Contexts;
using rental_car_api.Contexts.DTO;

namespace rental_car_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly Context _db;

        public ReservaController(Context db)
        {
            _db = db;
        }


        [HttpGet("obter-reservas-carro")]
        public async Task<IActionResult> ObterReservasCarroAsync(CancellationToken ct, int idCarro)
        {
            try
            {
                var reservas = await _db.Reservas.AsNoTracking().Where(x => x.IdCarro == idCarro)
                                    .Select(x => new ReservaCarroResponse
                                    {
                                        DataInicio = x.DataInicio,
                                        DataFim = x.DataFim
                                    }).ToListAsync(ct);

                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-reservas-usuario")]
        public async Task<IActionResult> ObterReservasUsuarioAsync(CancellationToken ct)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name!, ct);

                var reservas = await (from reserva in _db.Reservas
                                      join carro in _db.Carros on reserva.IdCarro equals carro.Id
                                      where reserva.IdUsuario == user!.Id
                                      select new ReservaUsuarioResponse
                                      {
                                          IdReserva = reserva.Id,
                                          IdCarro = carro.Id,
                                          MarcaCarro = carro.Marca,
                                          ModeloCarro = carro.Modelo,
                                          DataInicio = reserva.DataInicio,
                                          DataFim = reserva.DataFim
                                      }).ToListAsync(ct);

                return Ok(reservas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("fazer-reserva")]
        public async Task<IActionResult> FazerReservaAsync(CancellationToken ct, ReservaRequest model)
        {
            try
            {
                var existReserva = await _db.Reservas.AnyAsync(x => x.IdCarro == model.IdCarro && model.DataInicio <= x.DataFim && model.DataFim >= x.DataInicio);

                if (existReserva)
                    return BadRequest("Periodo já está reservado.");

                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name!, ct);

                ReservaModel reserva = new ReservaModel
                {
                    IdCarro = model.IdCarro,
                    IdUsuario = user!.Id,
                    DataInicio = model.DataInicio,
                    DataFim = model.DataFim
                };

                await _db.Reservas.AddAsync(reserva, ct);
                await _db.SaveChangesAsync(ct);

                return Ok("Reserva realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("alterar-reserva")]
        public async Task<IActionResult> AlterarReservaAsync(CancellationToken ct, ReservaRequest model)
        {
            try
            {
                var existReserva = await _db.Reservas.AnyAsync(x => x.Id != model.IdReserva && x.IdCarro == model.IdCarro && model.DataInicio <= x.DataFim && model.DataFim >= x.DataInicio);

                if (existReserva)
                    return BadRequest("Periodo já está reservado.");

                var reserva = await _db.Reservas.FirstOrDefaultAsync(x => x.Id == model.IdReserva, ct);

                if (reserva == null)
                    return BadRequest("Nenhuma reserva encontrada.");

                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name!, ct);

                reserva.IdCarro = model.IdCarro;
                reserva.IdUsuario = user!.Id;
                reserva.DataInicio = model.DataInicio;
                reserva.DataFim = model.DataFim;

                _db.Reservas.Update(reserva);
                await _db.SaveChangesAsync(ct);

                return Ok("Reserva realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("excluir-reserva")]
        public async Task<IActionResult> ExcluirReservaAsync(CancellationToken ct, int id)
        {
            try
            {
                var reserva = await _db.Reservas.FirstOrDefaultAsync(x => x.Id == id, ct);

                if (reserva == null)
                    return BadRequest("Nenhuma reserva encontrada.");

                _db.Reservas.Remove(reserva);
                await _db.SaveChangesAsync(ct);

                return Ok("A reserva foi excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
