using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AudioPostprocessor : AssetPostprocessor {

	const string AudioFolderName = "audio/";
	const string FxFolderName = "fx/";
	const string MusicFolderName = "music/";

	void OnPreprocessAudio()
	{
		if(isInFolder(FxFolderName))
		{
			//SoundFX configuration
			AudioImporter audioImporter = (AudioImporter)assetImporter;
			audioImporter.forceToMono = true;

			AudioImporterSampleSettings settings = new AudioImporterSampleSettings();
			settings.loadType = AudioClipLoadType.DecompressOnLoad;
			settings.compressionFormat = AudioCompressionFormat.PCM;
			settings.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;

			audioImporter.defaultSampleSettings = settings;
		}
		else if(isInFolder(MusicFolderName))
		{
			//Music configuration
			AudioImporter audioImporter = (AudioImporter)assetImporter;
			audioImporter.forceToMono = true;

			AudioImporterSampleSettings settings = new AudioImporterSampleSettings();
			settings.loadType = AudioClipLoadType.CompressedInMemory;
			settings.compressionFormat = AudioCompressionFormat.Vorbis;
			settings.quality = 100.0f;
			settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

			audioImporter.defaultSampleSettings = settings;
		}
	}

	private bool isInFolder(params string[] args)
	{
		string lowerCaseAssetPath = assetPath.ToLower();
		string folderPath = string.Empty;
		foreach(string folderName in args)
		{
			folderPath += folderName;
		}

		return lowerCaseAssetPath.IndexOf("/" + AudioFolderName + folderPath) != -1;
	}
}
