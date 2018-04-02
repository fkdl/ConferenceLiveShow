using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConferenceLiveShow.Models;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace ConferenceLiveShow.Services
{
    public class FaceApiHelper
    {
        private FaceServiceClient _serviceClient;
        private Queue<DateTime> _timeStampQueue = new Queue<DateTime>();
        private Windows.Storage.ApplicationDataContainer _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public FaceApiHelper()
        {
            if (_localSettings.Values["FaceAPIKey"] == null || _localSettings.Values["EndPoint"] == null)
            {
                throw new Exception("Cannot find api key or end point.");
            }

            _serviceClient = new FaceServiceClient(_localSettings.Values["FaceAPIKey"].ToString(), _localSettings.Values["EndPoint"].ToString());
        }

        public async Task<CustomFaceModel[]>  GetDetectEmojiAsync(Stream picture)
        {
            CustomFaceModel[] customFaceModels = null;
            try
            {
                // await WaitIfOverCallLimitAsync();
                var requiredFaceAttributes = new FaceAttributeType[] { FaceAttributeType.Emotion };
                Face[] detectResults = await _serviceClient.DetectAsync(picture, false, true, returnFaceAttributes: requiredFaceAttributes);
                customFaceModels = new CustomFaceModel[detectResults.Length];
                for(int i = 0; i< detectResults.Length; i++)
                {

                    FaceRectangle rectangle = detectResults[i].FaceRectangle;
                    CustomFaceModel model = new CustomFaceModel()
                    {
                        Anger = detectResults[i].FaceAttributes.Emotion.Anger,
                        Contempt = detectResults[i].FaceAttributes.Emotion.Contempt,
                        Disgust = detectResults[i].FaceAttributes.Emotion.Disgust,
                        Fear = detectResults[i].FaceAttributes.Emotion.Fear,
                        Happiness = detectResults[i].FaceAttributes.Emotion.Happiness,
                        Neutral = detectResults[i].FaceAttributes.Emotion.Neutral,
                        Sadness = detectResults[i].FaceAttributes.Emotion.Sadness,
                        Suprise = detectResults[i].FaceAttributes.Emotion.Surprise,
                        Top = rectangle.Top,
                        Left = rectangle.Left,
                        Width = rectangle.Width,
                        Height = rectangle.Height
                    };
                    customFaceModels[i] = model;
                }
            }
            catch (Exception)
            {
                // Just catch it.
            }

            return customFaceModels;
        }

        //public async Task<CustomFaceEmojiModel> GetEmojiResult(CustomFaceModel[] emojiModel)
        //{
        //    CustomFaceEmojiModel customFaceEmojiModel = new CustomFaceEmojiModel();
        //    customFaceEmojiModel.Emojis = new EmojiNum[emojiModel.Length];
        //    float upperleft = 0, upperrignt = 0, buttomleft = 0, buttomright = 0, averageX = 0, averageY = 0;
        //    foreach(var eachemoliModel in emojiModel)
        //    {
        //        averageX += eachemoliModel.Left;
        //        averageY += eachemoliModel.Top;
        //    }
        //    averageX /= emojiModel.Length;
        //    averageY /= emojiModel.Length;

        //    for(int i = 0; i< emojiModel.Length; i++)
        //    {
        //        customFaceEmojiModel.Emojis[i].Emoji = -1 * (emojiModel[i].Anger + emojiModel[i].Contempt + emojiModel[i].Disgust + emojiModel[i].Fear + emojiModel[i].Sadness) + emojiModel[i].Happiness + emojiModel[i].Neutral + emojiModel[i].Suprise;
        //        customFaceEmojiModel.EmojiSum += customFaceEmojiModel.Emojis[i].Emoji;
        //        if (emojiModel[i].Left < averageX && emojiModel[i].Top > averageY)
        //        {
        //            upperleft += customFaceEmojiModel.Emojis[i].Emoji;
        //        }
        //        else if(emojiModel[i].Left < averageX && emojiModel[i].Top < averageY)
        //        {
        //            buttomleft += customFaceEmojiModel.Emojis[i].Emoji;
        //        }
        //        else if (emojiModel[i].Left > averageX && emojiModel[i].Top > averageY)
        //        {
        //            upperrignt += customFaceEmojiModel.Emojis[i].Emoji;
        //        }
        //        else if(emojiModel[i].Left > averageX && emojiModel[i].Top < averageY)
        //        {
        //            buttomright += customFaceEmojiModel.Emojis[i].Emoji;
        //        }
        //    }
        //    customFaceEmojiModel.UpperLeft /= upperleft;
        //    customFaceEmojiModel.ButtomLeft /= buttomleft;
        //    customFaceEmojiModel.UpperRight /= upperrignt;
        //    customFaceEmojiModel.ButtoRight /= buttomright;
        //    return customFaceEmojiModel;
        //}


        

        //public async Task<CustomFaceModel[]> GetIdentifyResultAsync(Stream picture)
        //{
        //    CustomFaceModel[] customFaceModels = null;
        //    try
        //    {
        //        // await WaitIfOverCallLimitAsync();
        //        Face[] detectResults = await _serviceClient.DetectAsync(picture);

        //        Guid[] guids = detectResults.Select(x => x.FaceId).ToArray();
        //        IdentifyResult[] identifyResults = await _serviceClient.IdentifyAsync(_groupId, guids);

        //        customFaceModels = new CustomFaceModel[detectResults.Length];
        //        for (int i = 0; i < identifyResults.Length; i++)
        //        {
        //            FaceRectangle rectangle = detectResults[i].FaceRectangle;

        //            // Set initial name to Unknown.
        //            string name = "Unknown";
        //            try
        //            {
        //                name = (await _serviceClient.GetPersonAsync(_groupId, identifyResults[i].Candidates[0].PersonId)).Name;
        //            }
        //            catch (Exception)
        //            {
        //                // Just catch person not found.
        //                // It will return a name "Unknown" for this one.
        //            }

        //            CustomFaceModel model = new CustomFaceModel()
        //            {
        //                Name = name,
        //                Top = rectangle.Top,
        //                Left = rectangle.Left,
        //                Width = rectangle.Width,
        //                Height = rectangle.Height
        //            };

        //            customFaceModels[i] = model;
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        // Just catch it.
        //    }

        //    return customFaceModels;
        //}

    }
}
