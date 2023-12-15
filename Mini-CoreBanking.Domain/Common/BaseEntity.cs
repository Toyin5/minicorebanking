using System.ComponentModel.DataAnnotations;
using MiniCoreBanking.Domain.Constants;

namespace MiniCoreBanking.Domain.Common;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public StatusTypes Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}