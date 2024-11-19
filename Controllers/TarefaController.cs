
       using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            // okTODO: Buscar o Id no banco utilizando o EF
            // okTODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // ok caso contrário retornar OK com a tarefa encontrada
            if (tarefa == null)
                return NotFound ();
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
             var tarefas = _context.Tarefas.ToList();
            // okTODO: Buscar todas as tarefas no banco utilizando o EF
            return Ok(tarefas);
        }

[HttpGet("ObterPorTitulo")]
public IActionResult ObterPorTitulo(string titulo)
{

    var tarefas = _context.Tarefas
        .Where(x => x.Titulo.ToLower().Contains(titulo.ToLower()))
        .ToList();
            // okTODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // okDica: Usar como exemplo o endpoint ObterPorData


    return Ok(tarefas);
}


        [HttpGet("ObterPorData")]

        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }


    [HttpGet("ObterPorStatus")]
    public IActionResult ObterPorStatus(EnumStatusTarefa status)
    {
            // okTODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // okDica: Usar como exemplo o endpoint ObterPorData
        var tarefas = _context.Tarefas
        .Where(x => x.Status == status)
        .ToList();

        return Ok(tarefas);
    }


[HttpPost]
public async Task<IActionResult> Criar(Tarefa tarefa)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (tarefa.Data == DateTime.MinValue)
    {
        return BadRequest("A data da tarefa não pode ser vazia");
    }
    // okTODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
    try
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao criar a tarefa: {ex.Message}");
    }
}


[HttpPut("{id}")]
public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
{
    var tarefaBanco = await _context.Tarefas.FindAsync(id);

    if (tarefaBanco == null)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (tarefa.Data == DateTime.MinValue)
    {
        return BadRequest("A data da tarefa não pode ser vazia");
    }

    tarefaBanco.Titulo = tarefa.Titulo;
    tarefaBanco.Descricao = tarefa.Descricao;
    // Atualizar outras propriedades da tarefa...

    try
    {
        await _context.SaveChangesAsync();
        return Ok(tarefaBanco);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao atualizar a tarefa: {ex.Message}");
    }
            // okTODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // okTODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
}

[HttpDelete("{id}")]
public async Task<IActionResult> Deletar(int id)
{
    var tarefaBanco = await _context.Tarefas.FindAsync(id);

    if (tarefaBanco == null)
    {
        return NotFound();
    }

    try
    {
        _context.Tarefas.Remove(tarefaBanco);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    catch (Exception ex)
    {
        return StatusCode(500,   
 $"Erro ao deletar a tarefa: {ex.Message}");
    }
        // okTODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
}
  
    }
}
