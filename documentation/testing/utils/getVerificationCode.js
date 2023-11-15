/* The below is an example pertaining to retrieving an OTP code from an AWS SQS queue. 
NOTE: in order to retrieve the generated code, the right AWS role must be assumed to get access to the queue
 where the message is set up to land.
Liaising wth infrastructure on the project you're assigned to may be required to have the right setup
 with test email addresses/phone numbers */


import proxy from 'proxy-agent'
import AWS from 'aws-sdk'

if (process.env.DRONE_STAGE_NAME) {
  AWS.config.update({
    httpOptions: { agent: proxy('YOUR PROXY HERE') },
  })
}

const sqs = new AWS.SQS({ region: 'YOUR AWS REGION HERE' })
const queue_url = 'YOUR SQS QUEUE HERE'

export const getVerificationCode = async () => {
  let code

  while (!code) {
    const data = await sqs
      .receiveMessage({
        QueueUrl: queue_url,
        MaxNumberOfMessages: 10,
        VisibilityTimeout: 20,
        WaitTimeSeconds: 5,
      })
      .promise()
    const messages = data.Messages || []
    messages.forEach(async (message) => {
      console.log('------------------------------------')
      const messageBody = JSON.parse(message.Body)
      const email = JSON.parse(messageBody.Message)
      const found = email.content.split('INSERT STRING TO SPLIT FROM')[1].split('INSERT STRING TO SPLIT MESSAGE TO')[0];
      // the below is an example for searching a six digit code match in the message content we split above
      code = found.match(/\d\d\d\d\d\d/)[0];
      await sqs 
      .deleteMessage({
        QueueUrl: queue_url,
        ReceiptHandle: message.ReceiptHandle,
      })
      .promise()
    })
  }
  return code
}