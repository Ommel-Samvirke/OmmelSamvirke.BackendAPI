﻿namespace OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

public class PdfBlock : ContentBlock
{
    public PdfBlock(bool isOptional, int xPosition, int yPosition, int width, int? height)
        : base(isOptional, xPosition, yPosition, width, height)
    {
    }
    
    public PdfBlock(
        int id,
        DateTime dateCreated,
        DateTime dateModified,
        bool isOptional,
        int xPosition,
        int yPosition,
        int width,
        int? height
    ) : base(id, dateCreated, dateModified, isOptional, xPosition, yPosition, width, height)
    {
    }
}
