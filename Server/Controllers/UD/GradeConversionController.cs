﻿using Microsoft.AspNetCore.Mvc;
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

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeConversionController : BaseController, GenericRestController<GradeConversionDTO>
    {
        public GradeConversionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }
        [HttpDelete]
        [Route("Delete/{LetterGrade}")]
        public async Task<IActionResult> Delete(string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == LetterGrade).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeConversions.Remove(itm);
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

                var result = await _context.GradeConversions.Select(sp => new GradeConversionDTO
                {
                    LetterGrade = sp.LetterGrade,
                    ModifiedBy = sp.ModifiedBy,
                    CreatedDate = sp.CreatedDate,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    GradePoint = sp.GradePoint,
                    MaxGrade = sp.MaxGrade,
                    MinGrade = sp.MinGrade,
                    ModifiedDate = sp.ModifiedDate
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
        [HttpGet]
        [Route("Get/{SchoolId}/{LetterGrade}")]
        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> Get(int SchoolId, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeConversionDTO? result = await _context
                    .GradeConversions
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.LetterGrade == LetterGrade)
                     .Select(sp => new GradeConversionDTO
                     {
                          LetterGrade = LetterGrade,
                           ModifiedBy = sp.ModifiedBy,
                            CreatedDate = sp.CreatedDate,
                             SchoolId = SchoolId,
                              CreatedBy = sp.CreatedBy,
                               GradePoint = sp.GradePoint,
                                MaxGrade = sp.MaxGrade,
                                 MinGrade = sp.MinGrade,
                                  ModifiedDate = sp.ModifiedDate

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
        public async Task<IActionResult> Post([FromBody] GradeConversionDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == _T.LetterGrade).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeConversion c = new GradeConversion
                    {
                        LetterGrade = _T.LetterGrade,
                        SchoolId = _T.SchoolId,
                        GradePoint = _T.GradePoint,
                        MaxGrade = _T.MaxGrade,
                        MinGrade = _T.MinGrade,
                    };
                    _context.GradeConversions.Add(c);
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
        public async Task<IActionResult> Put([FromBody] GradeConversionDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.LetterGrade == _T.LetterGrade).FirstOrDefaultAsync();

                itm.LetterGrade = _T.LetterGrade;
                        itm.SchoolId = _T.SchoolId;
                        itm.GradePoint = _T.GradePoint;
                        itm.MaxGrade = _T.MaxGrade;
                        itm.MinGrade = _T.MinGrade;
                _context.GradeConversions.Update(itm);
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
