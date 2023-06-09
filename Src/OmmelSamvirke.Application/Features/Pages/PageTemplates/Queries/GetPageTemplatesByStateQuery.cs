﻿using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;

public class GetPageTemplatesByStateQuery : IRequest<List<PageTemplateWithoutContentBlocksDto>>
{
    public PageTemplateState PageTemplateState { get; }

    public GetPageTemplatesByStateQuery(PageTemplateState pageTemplateState)
    {
        PageTemplateState = pageTemplateState;
    }
}

public class GetPageTemplatesByStateQueryHandler : IRequestHandler<GetPageTemplatesByStateQuery, List<PageTemplateWithoutContentBlocksDto>>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public GetPageTemplatesByStateQueryHandler(IMapper mapper, IPageTemplateRepository pageTemplateRepository)
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
    }
    
    public async Task<List<PageTemplateWithoutContentBlocksDto>> Handle(
        GetPageTemplatesByStateQuery request,
        CancellationToken cancellationToken
    )
    {
        GetPageTemplatesByStateQueryValidator validator = new();
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        IReadOnlyList<PageTemplate> pageTemplates = await _pageTemplateRepository.GetAsync();
        List<PageTemplate> filteredPageTemplates = pageTemplates.Where(
            x => x.State == request.PageTemplateState
        ).ToList();
        
        return _mapper.Map<List<PageTemplateWithoutContentBlocksDto>>(filteredPageTemplates);
    }
}
