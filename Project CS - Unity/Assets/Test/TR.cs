using Microsoft.ML;
using MLNetML.Model;
using UnityEngine;
using System;
namespace MLNet
{
    class TR:MonoBehaviour
    {
        PredictionEngine<ModelInput, ModelOutput> predictionFunc;
        //public string Sample;
        public void Start()
        {
            DataViewSchema modelSchema;
            var mlContext = new MLContext();
            var MODEL_FILEPATH = Application.dataPath + "/StreamingAssets/Model/MLModel.zip";
            ITransformer trainedModel = mlContext.Model.Load(MODEL_FILEPATH, out modelSchema);
            predictionFunc = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);
        }



        public string Prediction(string _Input_Data)
        {
            PredictionEngine<ModelInput, ModelOutput> _PE = predictionFunc;
            string[] _Data_Strings = _Input_Data.Split(',');
            if(_Data_Strings.Length != 42)
            {
                return "NaA";
            }

            float[] _Data_Values = new float[_Data_Strings.Length];
            for (int j = 0; j < _Data_Values.Length; j++)
            {
                _Data_Values[j] = float.Parse(_Data_Strings[j]);
            }
            int i = 0;
            ModelInput sampleData = new ModelInput()
            {
                X0 = _Data_Values[i++],
                Y0 = _Data_Values[i++],
                X5 = _Data_Values[i++],
                Y5 = _Data_Values[i++],
                X10 = _Data_Values[i++],
                Y10 = _Data_Values[i++],
                X15 = _Data_Values[i++],
                Y15 = _Data_Values[i++],
                X20 = _Data_Values[i++],
                Y20 = _Data_Values[i++],
                X25 = _Data_Values[i++],
                Y25 = _Data_Values[i++],
                X30 = _Data_Values[i++],
                Y30 = _Data_Values[i++],
                X35 = _Data_Values[i++],
                Y35 = _Data_Values[i++],
                X40 = _Data_Values[i++],
                Y40 = _Data_Values[i++],
                X45 = _Data_Values[i++],
                Y45 = _Data_Values[i++],
                X50 = _Data_Values[i++],
                Y50 = _Data_Values[i++],
                X55 = _Data_Values[i++],
                Y55 = _Data_Values[i++],
                X60 = _Data_Values[i++],
                Y60 = _Data_Values[i++],
                X65 = _Data_Values[i++],
                Y65 = _Data_Values[i++],
                X70 = _Data_Values[i++],
                Y70 = _Data_Values[i++],
                X75 = _Data_Values[i++],
                Y75 = _Data_Values[i++],
                X80 = _Data_Values[i++],
                Y80 = _Data_Values[i++],
                X85 = _Data_Values[i++],
                Y85 = _Data_Values[i++],
                X90 = _Data_Values[i++],
                Y90 = _Data_Values[i++],
                X95 = _Data_Values[i++],
                Y95 = _Data_Values[i++],
                X100 = _Data_Values[i++],
                Y100 = _Data_Values[i],
            };
            var predictionResult = _PE.Predict(sampleData);
            Debug.Log(predictionResult.Prediction);
            for(int j = 0; j < predictionResult.Score.Length; j++)
            {
                Debug.Log(j + ": " + predictionResult.Score[j]);
            }
            return predictionResult.Prediction;
        }
    }
}
