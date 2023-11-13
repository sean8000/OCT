using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static Duende.IdentityServer.Models.IdentityResources;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : BaseController, GenericRestController<InstructorDTO>
    {
        public InstructorController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }
        [HttpDelete]
        [Route("Delete/{InstructorId}")]
        public async Task<IActionResult> Delete(int InstructorId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == InstructorId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Instructors.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Instructors.Select(sp => new InstructorDTO
                {
                    ModifiedDate = sp.ModifiedDate,
                    CreatedDate = sp.CreatedDate,
                    CreatedBy = sp.CreatedBy,
                    InstructorId = sp.InstructorId,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        public async Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("Get/{SchoolId}/{InstructorId}")]
        public async Task<IActionResult> Get(int SchoolId, int InstructorId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                InstructorDTO? result = await _context
                    .Instructors
                    .Where(x => x.InstructorId == InstructorId)
                    .Where(x => x.SchoolId == SchoolId)
                     .Select(sp => new InstructorDTO
                     {
                          ModifiedDate = sp.ModifiedDate,
                           CreatedDate = sp.CreatedDate,
                            CreatedBy = sp.CreatedBy,
                             InstructorId = InstructorId,
                              FirstName = sp.FirstName,
                              LastName = sp.LastName,
                               ModifiedBy = sp.ModifiedBy,
                                Phone = sp.Phone,
                                 Salutation = sp.Salutation,
                                  SchoolId = SchoolId,
                                   StreetAddress = sp.StreetAddress,
                                    Zip = sp.Zip
                                    
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] InstructorDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _T.InstructorId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Instructor c = new Instructor
                    {
                        InstructorId = _T.InstructorId,
                        FirstName = _T.FirstName,
                        LastName = _T.LastName,
                        Phone = _T.Phone,
                        Salutation = _T.Salutation,
                        SchoolId = _T.SchoolId,
                        StreetAddress = _T.StreetAddress,
                        Zip = _T.Zip
                    };
                    _context.Instructors.Add(c);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] InstructorDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _T.InstructorId).FirstOrDefaultAsync();

                itm.InstructorId = _T.InstructorId;
                        itm.FirstName = _T.FirstName;
                        itm.LastName = _T.LastName;
                        itm.Phone = _T.Phone;
                        itm.Salutation = _T.Salutation;
                        itm.SchoolId = _T.SchoolId;
                        itm.StreetAddress = _T.StreetAddress;
                        itm.Zip = _T.Zip;

                _context.Instructors.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}
