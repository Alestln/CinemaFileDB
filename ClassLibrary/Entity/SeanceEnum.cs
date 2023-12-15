using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Entity;

public enum SeanceEnum
{
    [Display(Name = "Денний")]
    Day = 1,
    [Display(Name = "Нічний")]
    Night
}