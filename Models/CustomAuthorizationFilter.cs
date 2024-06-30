using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? Idpropr  = context.HttpContext.Session.GetString("idpropr");
        string? idAdmin  = context.HttpContext.Session.GetString("id_admin");
        string? idClient  = context.HttpContext.Session.GetString("id_client");
        bool proprBool = string.IsNullOrEmpty(Idpropr);
        bool adminBool = string.IsNullOrEmpty(idAdmin);
        bool clientBool = string.IsNullOrEmpty(idClient);
        
        if (proprBool == true && adminBool == true && clientBool == true )
        {
            context.Result = new RedirectToActionResult("Index", "Client", null);
        }
        else if (proprBool == false || adminBool == true || clientBool == false)
        {
            // Console.WriteLine(statues);
            // Si la session n'existe pas, rediriger vers la page de connexion
            context.Result = new RedirectToActionResult("AuthentificationNotifPage", "Authentification", null);
        }
    }
}