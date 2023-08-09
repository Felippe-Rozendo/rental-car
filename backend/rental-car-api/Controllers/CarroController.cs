using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using rental_car_api.Contexts;
using rental_car_api.Contexts.DTO;
using rental_car_api.Enum;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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

        [AllowAnonymous]
        [HttpPost("obter-carros")]
        public async Task<IActionResult> ObterCarrosAsync(CancellationToken ct, [FromBody] FiltroCarroRequest? filtro)
        {
            try
            {
                var query = await _db.Carros.AsNoTracking()
                                            .Where(f =>
                                                (string.IsNullOrEmpty(filtro.Marca) || f.Marca.ToUpper().Contains(filtro.Marca.ToUpper())) &&
                                                (string.IsNullOrEmpty(filtro.Modelo) || f.Modelo.ToUpper().Contains(filtro.Modelo.ToUpper())) &&
                                                (!filtro.PotenciaMin.HasValue || f.Potencia >= filtro.PotenciaMin) &&
                                                (!filtro.PotenciaMax.HasValue || f.Potencia <= filtro.PotenciaMax) &&
                                                (!filtro.TorqueMin.HasValue || f.Torque >= filtro.TorqueMin) &&
                                                (!filtro.TorqueMax.HasValue || f.Torque <= filtro.TorqueMax) &&
                                                (string.IsNullOrEmpty(filtro.Combustivel) || f.Combustivel == filtro.Combustivel) &&
                                                (!filtro.PrecoDiariaMin.HasValue || f.PrecoDiaria >= filtro.PrecoDiariaMin) &&
                                                (!filtro.PrecoDiariaMax.HasValue || f.PrecoDiaria <= filtro.PrecoDiariaMax) &&
                                                (!filtro.AnoMin.HasValue || f.Ano >= filtro.AnoMin) &&
                                                (!filtro.AnoMax.HasValue || f.Ano <= filtro.AnoMax))
                                            .Select(x => new CarroDtoResponse
                                            {
                                                Id = x.Id,
                                                Ano = x.Ano,
                                                Combustivel = EnumHelper.GetEnumDescription<CombustivelEnum>(x.Combustivel),
                                                Fotos = FotosCarro(x.Marca, x.Modelo),
                                                Marca = x.Marca,
                                                Modelo = x.Modelo,
                                                Potencia = x.Potencia.ToString().Replace('.', ','),
                                                PrecoDiaria = x.PrecoDiaria.ToString().Replace('.', ','),
                                                Torque = x.Torque.ToString().Replace('.', ',')
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

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("upload/imagem-carro")]
        public async Task<IActionResult> UploadImagemAsync(IFormFile[] fotos, string marca, string modelo)
        {
            try
            {
                foreach (var foto in fotos)
                {
                    if (foto.Length > 0)
                    {
                        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", marca.ToLower().Replace(" ", ""), modelo.ToLower().Replace(" ", ""));

                        if (!Directory.Exists(uploadsFolderPath))
                        {
                            Directory.CreateDirectory(uploadsFolderPath);
                        }

                        var filePath = Path.Combine(uploadsFolderPath, foto.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }
                    }
                }

                return Ok("Fotos Salvas com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("excluir-foto")]
        public IActionResult ExcluirFoto(string fileName, string marca, string modelo)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", marca.ToLower().Replace(" ", ""), modelo.ToLower().Replace(" ", ""), fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("Foto excluída com sucesso");
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        private static List<string> FotosCarro(string marca, string modelo)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", marca.ToLower().Replace(" ", ""), modelo.ToLower().Replace(" ", ""));

            if (Directory.Exists(folderPath))
            {
                var imageFiles = Directory.GetFiles(folderPath);

                var imageUrls = new List<string>();

                foreach (var file in imageFiles)
                {
                    var fileName = Path.GetFileName(file);
                    var image = System.IO.File.ReadAllBytes(Path.Combine("Resources", marca.ToLower().Replace(" ", ""), modelo.ToLower().Replace(" ", ""), fileName));
                    imageUrls.Add(Convert.ToBase64String(image));
                }

                return imageUrls;
            }

            return new List<string>();
        }
    }
}
