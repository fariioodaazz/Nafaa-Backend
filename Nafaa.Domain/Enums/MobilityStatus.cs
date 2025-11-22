namespace Nafaa.Domain.Enums;

public enum MobilityStatus
{
    Independent,            // مستقل الحركة
    NeedsAssistance,        // يحتاج مساعدة
    CaneOrWalker,           // يستخدم عصا/ووكر
    WheelchairUser,         // يستخدم كرسي متحرك
    Bedridden,              // طريح الفراش
    TemporaryInjury         // إصابة مؤقتة تؤثر على الحركة
}
