using Blazored.Modal;
using ClientNames.Extensions;
using DailyPlanService;
using DailyResultsService;
using DailyTriggerService;
using DatePicker;
using DefectService;
using DepartmentService;
using eDDS.WebTool.Scoped;
using LineAreaService;
using PeopleService;
using PlannedStopService;
using PlantService;
using ProductionLineService;
using ValueStreamService;
using ZoneTriggerService;


var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddPlantHttpClient(builder.Configuration.GetConnectionString("PlantModel"));
    builder.Services.AddDefectHttpClient(builder.Configuration.GetConnectionString("Defects"));
    builder.Services.AddPeopleHttpClient(builder.Configuration.GetConnectionString("People"));
    builder.Services.AddPlannedStopHttpClient(builder.Configuration.GetConnectionString("PlannedStop"));
    builder.Services.AddDailyPlannerHttpClient(builder.Configuration.GetConnectionString("DailyPlanner"));
    builder.Services.AddDailyTriggerHttpClient(builder.Configuration.GetConnectionString("DailyTrigger"));
    builder.Services.AddDailyResultsHttpClient(builder.Configuration.GetConnectionString("DailyResults"));
    builder.Services.AddZoneTriggerHttpClient(builder.Configuration.GetConnectionString("ZoneTrigger"));

    builder.Services.AddScoped<IPlantService,PlantService.Service>
        (sp => new PlantService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IValueStreamService,ValueStreamService.Service>(sp =>
        new ValueStreamService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IDepartmentService, DepartmentService.Service>
        (sp => new DepartmentService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IProductionLineService,ProductionLineService.Service>(sp =>
        new ProductionLineService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<ILineAreaService,LineAreaService.Service>
        (sp => new LineAreaService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IDefectService,DefectService.Service>
        (sp => new DefectService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IPeopleService,PeopleService.Service>
        (sp => new PeopleService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IDailyPlanService,DailyPlanService.Service>
        (sp => new DailyPlanService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IPlannedStopService,PlannedStopService.Service>
        (sp => new PlannedStopService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IDailyTriggerService, DailyTriggerService.Service>
        (sp => new DailyTriggerService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<ISafetyService,SafetyService>
        (sp => new SafetyService(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped<IDailyResultsService,DailyResultsService.Service >
        (sp => new DailyResultsService.Service(sp.GetRequiredService<IHttpClientFactory>()));

    builder.Services.AddScoped(sp => new IsBusyScoped());

    builder.Services.AddBlazoredModal();

    builder.Services
        .AddScoped<LevelNavigationObject>();
    builder.Services.AddScoped<DateNavigationObject>();

    var app = builder.Build();

    app.UseStaticFiles();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
