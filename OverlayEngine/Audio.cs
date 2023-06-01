using System.IO;
using Aud.IO;
using Aud.IO.Formats;
using FragLabs.Audio.Engines;
using FragLabs.Audio.Engines.OpenAL;

namespace OverlayEngine
{
    public static class Audio
    {
        static PlaybackStream playback;

        public static void PlayAudio(AudioFile file)
        {
            playback = OpenALHelper.PlaybackDevices[0].OpenStream(file.SampleRate, OpenALAudioFormat.Stereo16Bit);

            byte[] rawAudio = file.GetModulatedAudio();

            playback.Listener.Position = new Vector3() { X = 0.0f, Y = 0.0f, Z = 0.0f };
            playback.Listener.Velocity = new Vector3() { X = 0.0f, Y = 0.0f, Z = 0.0f };
            playback.Listener.Orientation = new Orientation()
            {
                At = new Vector3() { X = 0.0f, Y = 0.0f, Z = 1.0f },
                Up = new Vector3() { X = 0.0f, Y = 1.0f, Z = 0.0f }
            };
            playback.ALPosition = new Vector3() { X = 0.0f, Y = 0.0f, Z = 0.0f };
            playback.Velocity = new Vector3() { X = 0.0f, Y = 0.0f, Z = 0.0f };

            playback.Write(rawAudio, 0, rawAudio.Length);

            //Thread.Sleep(1000);
            //playback.Close();
        }

        public static WaveFile LoadWaveFile(string filePath) => new WaveFile(filePath);

        public static WaveFile LoadWaveFile(Stream file)
        {
            string tempPath = Path.GetTempFileName();
            var fs = File.OpenWrite(tempPath);

            file.CopyTo(fs);

            fs.Close();

            return new WaveFile(tempPath);
        }
    }
}
