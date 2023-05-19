﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    [Table("CourseFeedback")]
    [Index(nameof(CourseId), Name = "idx_coursefeedback_courseid")]
    public partial class CourseFeedback
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int InstructorId { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal InstructorRating { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal CourseRating { get; set; }
        [StringLength(2048)]
        public string FeedbackText { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(InstructorId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Instructor Instructor { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty("CourseFeedbacks")]
        public virtual Student Student { get; set; }
    }
}