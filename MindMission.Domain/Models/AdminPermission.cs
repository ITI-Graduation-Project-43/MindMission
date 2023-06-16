﻿using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    [Index(nameof(Id), Name = "idx_adminpermissions_adminid")]
    public partial class AdminPermission : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }

        [Key]
        public int PermissionId { get; set; }

        public DateTime GrantedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Id))]
        [InverseProperty("AdminPermissions")]
        public virtual Admin Admin { get; set; }

        [ForeignKey(nameof(PermissionId))]
        [InverseProperty("AdminPermissions")]
        public virtual Permission Permission { get; set; }
    }
}