﻿namespace LitZhu.DomainCommons.Models;

public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; }

}
