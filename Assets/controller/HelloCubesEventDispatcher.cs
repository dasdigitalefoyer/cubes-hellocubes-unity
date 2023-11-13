using System.Collections;
using System.Collections.Generic;
using MQTTnet;
using Newtonsoft.Json;
using PuzzleCubes.Communication;
using UnityEngine;

public class HelloCubesEventDispatcher : EventDispatcher
{
   public HelloCubesEvent helloCubesEvent;

   public const string helloCubesTopic =  "puzzleCubes/helloCubes";

    

    protected override void Initialize()
    {
        base.Initialize();

        // SAMPLE EVENT DISPATCHER - BEGIN

        // FOR JSON MESSAGE
        //jsonTypeToEventMap.Add(typeof(HelloCubes), (x, o) => (x as HelloCubesEventDispatcher).helloCubesEvent.Invoke(o as HelloCubes));

        // SAMPLE EVENT DISPATCHER - END


        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(helloCubesTopic).Build() ,HandleHelloCubes);
    }

    // SAMPLE HANDLER FOR MQTT MESSAGE
    public void HandleHelloCubes(MqttApplicationMessage msg, IList<string> wildcardItem){
         var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<HelloCubes>(data);
        helloCubesEvent.Invoke( result);
    }


    public void DispatchHelloCubes(HelloCubes hc)
    {
        Debug.Log("DispatchHelloCubes");       
        var json = JsonConvert.SerializeObject(hc, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Objects
        });
        // this.SendZmq(json, true);  
        var msg = new MqttApplicationMessage();
        msg.Topic = helloCubesTopic;
        msg.Payload = System.Text.Encoding.UTF8.GetBytes(json);
        msg.MessageExpiryInterval = 3600;
        msg.Retain = true;
        
        this.mqttCommunication.Send(msg); 
        
        
    }


}
