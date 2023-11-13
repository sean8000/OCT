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
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SectionId}")]
        public async Task<IActionResult> Delete(int SectionId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == SectionId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
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

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime
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

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get/{SchoolId}/{SectionId}")]
        public async Task<IActionResult> Get(int SectionId, int SchoolId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context
                    .Sections
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.SchoolId == SchoolId)
                     .Select(sp => new SectionDTO
                     {
                            SchoolId = SchoolId,
                             SectionId = SectionId,
                              Capacity = sp.Capacity,
                               CourseNo = sp.CourseNo,
                                CreatedBy = sp.CreatedBy,
                                 CreatedDate = sp.CreatedDate,
                                  InstructorId = sp.InstructorId,
                                   Location = sp.Location,
                                    ModifiedBy = sp.ModifiedBy,
                                     ModifiedDate = sp.ModifiedDate,
                                      SectionNo = sp.SectionNo,
                                       StartDateTime = sp.StartDateTime
                                
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
        public async Task<IActionResult> Post([FromBody] SectionDTO _T)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == _T.SectionId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    EF.Models.Section c = new EF.Models.Section
                    {
                        SchoolId = _T.SchoolId,
                        SectionId = _T.SectionId,
                        Capacity = _T.Capacity,
                        CourseNo = _T.CourseNo,
                        CreatedBy = _T.CreatedBy,
                        CreatedDate = _T.CreatedDate,
                        InstructorId = _T.InstructorId,
                        Location = _T.Location,
                        ModifiedBy = _T.ModifiedBy,
                        ModifiedDate = _T.ModifiedDate,
                        SectionNo = _T.SectionNo,
                        StartDateTime = _T.StartDateTime
                    };
                    _context.Sections.Add(c);
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
        public async Task<IActionResult> Put([FromBody] SectionDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.CourseNo == _T.SectionId).FirstOrDefaultAsync();

                        itm.SchoolId = _T.SchoolId;
                        itm.SectionId = _T.SectionId;
                        itm.Capacity = _T.Capacity;
                        itm.CourseNo = _T.CourseNo;
                        itm.CreatedBy = _T.CreatedBy;
                        itm.CreatedDate = _T.CreatedDate;
                        itm.InstructorId = _T.InstructorId;
                        itm.Location = _T.Location;
                        itm.ModifiedBy = _T.ModifiedBy;
                        itm.ModifiedDate = _T.ModifiedDate;
                        itm.SectionNo = _T.SectionNo;
                        itm.StartDateTime = _T.StartDateTime;

                _context.Sections.Update(itm);
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
