using System.Media;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static void AudioPlayBeep() {
         SystemSounds.Beep.Play();
      }

      internal static void AudioPlayAsterisk() {
         SystemSounds.Asterisk.Play();
      }

      internal static void AudioPlayExclamation() {
         SystemSounds.Exclamation.Play();
      }

      internal static void AudioPlayHand() {
         SystemSounds.Hand.Play();
      }

      internal static void AudioPlayQuestion() {
         SystemSounds.Question.Play();
      }
   }
}
