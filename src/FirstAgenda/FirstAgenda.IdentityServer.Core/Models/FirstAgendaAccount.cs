﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using IdentityModel;

namespace FirstAgenda.IdentityServer.Core.Models
{
    /// <summary>
    /// In-memory user object for testing. Not intended for modeling users in production.
    /// </summary>
    public class FirstAgendaAccount
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        [Column("AdminEmail")]
        public string SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Column("Brugernavn")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Column("Adgangskode")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the provider name.
        /// </summary>
        [Column("IdsExternalProviderName")]
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the provider subject identifier.
        /// </summary>
        [Column("IdsExternalProviderSubjectId")]
        public string ProviderSubjectId { get; set; }

        /// <summary>
        /// Gets or sets if the user is active.
        /// </summary>
        [Column("Spaeret")]
        public bool IsActive { get; set; } = true;

        [Column("Salt")]
        public string Salt { get; set; }

        public DateTimeOffset LastPasswordChangeDateUtc { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }
        public Guid Uid { get; set; }
        public bool SkalSkiftePassword { get; set; }
        public int AntalFejlForsoeg { get; set; }
        public int LanguageID { get; set; }
        public string TimeZoneId { get; set; }
        public bool HarBillede { get; set; }

        //
        // /// <summary>
        // /// Gets or sets the claims.
        // /// </summary>
        // public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>(new ClaimComparer());
    }
}