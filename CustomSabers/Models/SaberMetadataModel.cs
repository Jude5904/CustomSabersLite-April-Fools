﻿using Newtonsoft.Json;
using System;

namespace CustomSabersLite.Models;

[method: JsonConstructor]
public class SaberMetadataModel(string relativePath, string hash, CustomSaberType saberType, SaberLoaderError loaderError, string saberName, string authorName, DateTime date)
{
    [JsonProperty("path")]
    public string RelativePath { get; } = relativePath;

    [JsonProperty("hash")]
    public string Hash { get; } = hash;

    [JsonProperty("type")]
    public CustomSaberType SaberType { get; } = saberType;

    [JsonProperty("error")]
    public SaberLoaderError LoaderError { get; } = loaderError;

    [JsonProperty("date")]
    public DateTime DateAdded { get; } = date;

    [JsonProperty("name")]
    public string SaberName { get; } = saberName;

    [JsonProperty("author")]
    public string AuthorName { get; } = authorName;

    /*[JsonProperty("hasIncompatibleShaders")]
    public bool IncompatibleShaders { get; } = incompatibleShaders;

    [JsonProperty("incompatibleShaderNames")]
    public string[] IncompatibleShaderNames { get; } = incompatibleShaderNames;*/
}
