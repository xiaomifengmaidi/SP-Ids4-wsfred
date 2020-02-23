﻿using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.WsFederation.Stores;
using System.Collections.Generic;

using System.IdentityModel.Tokens;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.WsFederation
{
    public class Config
    {
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
            // The sub/nameid claim
            new IdentityResources.OpenId(),
 
            // All claim for user profile info (think name, email, etc.)
            new IdentityResources.Profile()
        };
        }

        public static List<Client> GetClients()
        {
            return new List<Client> {
            new Client {
                // The realm of your RP
                ClientId = "urn:sharepoint",
 
                // Required for ws-fed clients
                ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
 
                // Trust uri of your SharePoint web application (web app, appended with _trust/default.aspx)
                RedirectUris = { "http://29504w97k2.wicp.vip:21368/_trust/default.aspx" },
 
                // SAML token lifetime (in seconds)
                IdentityTokenLifetime = 36000,
 
                // Links to configured resources
                AllowedScopes = {"openid", "profile"}
            }
        };
        }

        public static List<RelyingParty> GetRelyingParties()
        {
            return new List<RelyingParty> {
            new RelyingParty {
                // Same as ClientId. Used to link config
                Realm = "urn:sharepoint",
 
                // SAML 1.1 token type required by SharePoint
                TokenType = WsFederationConstants.TokenTypes.Saml11TokenProfile11,
 
                // Transform claim types from oidc standard to xml types
                // Only mapped claims will be returned for SAML 1.1 tokens
                ClaimMapping = new Dictionary<string, string> {
                    {JwtClaimTypes.Subject, ClaimTypes.NameIdentifier},
                    {JwtClaimTypes.Email, ClaimTypes.Email}
                },
 
                // Defaults
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString
            }
        };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
        new TestUser {
            SubjectId = "amadmin",
            Username = "amadmin",
            Password = "abc123#",
            Claims = new List<Claim> {new Claim(JwtClaimTypes.Email, "amadmin@sp.com") }
        }
    };
        }



    };
        
    
}