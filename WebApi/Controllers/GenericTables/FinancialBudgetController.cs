using Application.Interface.Services.GenericTables;
using Application.Models.GenericTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.GenericTables
{
    [Authorize]
    [Route("api/financial-budget")]
    [ApiController]
    public class FinancialBudgetController : ControllerBase
    {
        private readonly IFinancialBudgetService _service;

        public FinancialBudgetController(IFinancialBudgetService service)
        {
            _service = service;
        }

        // ===== PARENT ENTITY OPERATIONS =====

        /// <summary>
        /// Create a new financial budget entry
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinancialBudgetCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get financial budget by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get complete financial budget with all child entities
        /// </summary>
        [HttpGet("{id}/complete")]
        public async Task<IActionResult> GetComplete(int id)
        {
            var result = await _service.GetCompleteByIdAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update financial budget
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FinancialBudgetUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete financial budget
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ===== PAGINATION =====

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        [HttpGet("my-history")]
        [Authorize(Roles = "Trainer,UnitHead")]
        public async Task<IActionResult> GetMyHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get paginated financial budgets
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null)
        {
            var result = await _service.GetPaginatedAsync(pageNumber, pageSize, startDate, endDate);
            return Ok(result);
        }

        /// <summary>
        /// Get financial budgets by status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(
            string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
            return Ok(result);
        }

        // ===== STATUS MANAGEMENT =====
        // Note: No submit endpoint - Create/Update automatically sets to Pending

        /// <summary>
        /// Approve financial budget
        /// </summary>
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromBody] string? remarks = null)
        {
            var result = await _service.ApproveAsync(id, remarks);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Reject financial budget
        /// </summary>
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] string remarks)
        {
            var result = await _service.RejectAsync(id, remarks);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ===== CHILD ENTITY OPERATIONS =====

        /// <summary>
        /// Add budget to financial budget
        /// </summary>
        [HttpPost("{id}/budgets")]
        public async Task<IActionResult> AddBudget(int id, [FromBody] BudgetCreateDto dto)
        {
            var result = await _service.AddBudgetAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Add revolving fund to financial budget
        /// </summary>
        [HttpPost("{id}/revolving-funds")]
        public async Task<IActionResult> AddRevolvingFund(int id, [FromBody] RevolvingFundCreateDto dto)
        {
            var result = await _service.AddRevolvingFundAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Add bank account to financial budget
        /// </summary>
        [HttpPost("{id}/bank-accounts")]
        public async Task<IActionResult> AddBankAccount(int id, [FromBody] BankAccountCreateDto dto)
        {
            var result = await _service.AddBankAccountAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update budget
        /// </summary>
        [HttpPut("budgets/{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] BudgetCreateDto dto)
        {
            var result = await _service.UpdateBudgetAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update revolving fund
        /// </summary>
        [HttpPut("revolving-funds/{id}")]
        public async Task<IActionResult> UpdateRevolvingFund(int id, [FromBody] RevolvingFundCreateDto dto)
        {
            var result = await _service.UpdateRevolvingFundAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update bank account
        /// </summary>
        [HttpPut("bank-accounts/{id}")]
        public async Task<IActionResult> UpdateBankAccount(int id, [FromBody] BankAccountCreateDto dto)
        {
            var result = await _service.UpdateBankAccountAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete budget
        /// </summary>
        [HttpDelete("budgets/{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var result = await _service.DeleteBudgetAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete revolving fund
        /// </summary>
        [HttpDelete("revolving-funds/{id}")]
        public async Task<IActionResult> DeleteRevolvingFund(int id)
        {
            var result = await _service.DeleteRevolvingFundAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete bank account
        /// </summary>
        [HttpDelete("bank-accounts/{id}")]
        public async Task<IActionResult> DeleteBankAccount(int id)
        {
            var result = await _service.DeleteBankAccountAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ===== 🔥 HYBRID ENDPOINT - INSERT ALL DATA AT ONCE =====

        /// <summary>
        /// HYBRID CREATE: Create financial budget with all child entities in one request
        /// This endpoint allows you to insert parent + budgets + revolving funds + bank accounts all at once
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/financial-budget/hybrid
        ///     {
        ///       "unitLocationId": 1,
        ///       "startDate": "2024-01-01",
        ///       "endDate": "2024-12-31",
        ///       "budgets": [
        ///         {
        ///           "particulars": "Staff Salary",
        ///           "abac": 100000.00,
        ///           "dac": 50000.00,
        ///           "sanctioned": 150000.00,
        ///           "released": 100000.00,
        ///           "expenditure": 80000.00,
        ///           "balance": 20000.00
        ///         }
        ///       ],
        ///       "revolvingFunds": [
        ///         {
        ///           "yearMonth": "2024-01",
        ///           "openingBalance": 50000.00,
        ///           "expenditure": 10000.00,
        ///           "receipt": 15000.00,
        ///           "closingBalance": 55000.00
        ///         }
        ///       ],
        ///       "bankAccounts": [
        ///         {
        ///           "nameOfBank": "State Bank of India",
        ///           "locationBranch": "Main Branch",
        ///           "branchCode": "001234",
        ///           "accountName": "Unit Account",
        ///           "accountNumber": "1234567890",
        ///           "micrNumber": "500002001",
        ///           "ifscCode": "SBIN0001234"
        ///         }
        ///       ]
        ///     }
        ///
        /// </remarks>
        [HttpPost("hybrid")]
        public async Task<IActionResult> CreateHybrid([FromBody] FinancialBudgetHybridCreateDto dto)
        {
            var result = await _service.CreateHybridAsync(dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// HYBRID UPDATE: Update financial budget with all child entities in one request
        /// This endpoint allows you to update parent + manage all children (create/update/delete) at once
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/financial-budget/1/hybrid
        ///     {
        ///       "startDate": "2024-01-01",
        ///       "endDate": "2024-12-31",
        ///       "budgets": [
        ///         {
        ///           "id": 5,  // Existing budget to update
        ///           "particulars": "Staff Salary - Updated",
        ///           "abac": 120000.00,
        ///           "dac": 60000.00
        ///         },
        ///         {
        ///           "id": null,  // New budget to create
        ///           "particulars": "New Budget Item",
        ///           "abac": 50000.00
        ///         }
        ///       ],
        ///       "revolvingFunds": [...],
        ///       "bankAccounts": [...]
        ///     }
        ///
        /// Note: Child items not included in the request will be deleted
        /// </remarks>
        [HttpPut("{id}/hybrid")]
        public async Task<IActionResult> UpdateHybrid(int id, [FromBody] FinancialBudgetHybridUpdateDto dto)
        {
            var result = await _service.UpdateHybridAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ===== HELPER =====

        private int GetStatusCode(ServiceErrorStatus? status)
        {
            return status switch
            {
                ServiceErrorStatus.NOTFOUND => 404,
                ServiceErrorStatus.FORBIDDEN => 403,
                ServiceErrorStatus.BADREQUEST => 400,
                ServiceErrorStatus.INVALIDOPERATION => 400,
                _ => 500
            };
        }
    }
}
