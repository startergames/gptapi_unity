using Unity.Plastic.Newtonsoft.Json;

namespace unity_gpt_api.Runtime.Options {
    public class GptOption_Edit
    {
        [JsonProperty("model")]
        public string Model { get; set; }  // ID of the model to use. Options include text-davinci-edit-001 or code-davinci-edit-001.

        [JsonProperty("input")]
        public string Input { get; set; } = "";  // The input text to use as a starting point for the edit.

        [JsonProperty("instruction")]
        public string Instruction { get; set; }  // Instruction that tells the model how to edit the prompt.

        [JsonProperty("n")]
        public int N { get; set; } = 1;  // Number of edits to generate for the input and instruction.

        [JsonProperty("temperature")]
        public float Temperature { get; set; } = 1f;  // Determines output randomness. Higher value for more randomness, lower value for more determinism.

        [JsonProperty("top_p")]
        public float TopP { get; set; } = 1f;  // Nucleus sampling parameter. Lower values will limit output to a smaller vocabulary set.
    }
}