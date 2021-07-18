using System;
using System.Collections.Generic;
using Domain.Models;

namespace CareerGuidance.Models
{
    public enum ResultStateEnum
    {
        Created, 
        OnProgress,
        Cancelled,
        Finished
    }
    /*
    public class ResultDTO
    {
        public int? StatusId { get; set; }
        public string Status { get; set; }
        public DateTime? EndDate { get; set; }
        
        public TestResultDTO TestResult { get; set; }
        
        public static ResultDTO Create(Result rt)
        {
            return new() {
                StatusId = rt.StatusId,
                EndDate = rt.EndDate
            };
        }
    }
    */

    public class TestResultDTO
    {
        public int TestId { get; set; }
        public int ModalityId { get; set; }
        //public List<ResponseDTO> Response { get; set; }
        public int Total { get; set; }
        
        public bool IsLast { get; set; }
        
        public static TestResultDTO Create(TestResult tr)
        {
            return new() {
                TestId = tr.TestId,
                Total = tr.Total,
                ModalityId = tr.ModalityId
            };
        }
    }

    public class ResponseDTO
    {
        public int Id { get; set; }
        public int TestResultId { get; set; }
        public int AlternativeId { get; set; }
        public int Score { get; set; }
        
        public static ResponseDTO Create(Response tr)
        {
            return new() {
                Id = tr.Id,
                TestResultId = tr.TestResultId,
                AlternativeId = tr.AlternativeId,
                Score = tr.Score,
            };
        }
    }
    
    public class RecomendationDTO
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int CareerId { get; set; }
        public string Comments { get; set; }
        
        public static RecomendationDTO Create(Recomendation tr)
        {
            return new() {
                Id = tr.Id,
                ResultId = tr.ResultId,
                CareerId = tr.CareerId,
                Comments = tr.Comments,
            };
        }
    }
}