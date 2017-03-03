
用例说明：
写一条信息操作。WriteMessage
需求：有4种信息处理：安全验证/加密处理/压缩处理/数字签名。有的信息需要经过其中的一种或几种处理后才能写入。

一般思考逻辑：如果要加密写，那么定义一个加密写的类，专门处理这种信息。如果又要加密，又要压缩，定义一个加密压缩类，还有次序问题先加密再压缩类，还是先压缩后加密类  and so on 。
这样的话，当处理方式增多的情况下，组合情况会急剧增多，一直添加子类的话，将会引起灾难。
（或者另一种写大类的方式，定义一个方法，传入操作类型type，根据type不同，类中不同的实现处理，这样会造成类过于复杂，而且也会面临组合问题2^n）

第一步思考：既然都是写信息，那么抽象出来一个接口IMessageWriter，大家都实现它。
第二步思考：既然都实现了同一个接口，那么在一个其他类接受IMessageWriter参数的时候，它们都可以传参进去。
第三步思考：既然这个类接受了IMessageWriter参数，那么它也就可以在内部实现IMessageWriter的方法，这样它本身也实现了IMessageWriter接口，这样它也可以传递了。
第四步思考：安全验证/加密处理/压缩处理/数字签名 只建立4个类，实现接口IMessageWriter，也都有个构造函数，接受IMessageWriter对象，
那么【安全验证】【加密处理】【压缩处理】【数字签名】类可以互相传递，同时还能接受其他IMessageWriter对象，像下面一样：
var msgWriter = new MessageWriter();//其他IMessageWriter
var decorateMsgWriter = new DigitallySignedDecorate(new EncryptedDecorate(new CompressedDecorate(new SecureDecorate(msgWriter))));
下面可以具体实现这4个类的接口实现了。在方法中，实现自己需要的单一职责后，传递给IMessageWriter对象，由它继续完成接口的实现，直至最终完成。

这就是装饰器模式，用例中抽象出了一个父类Decorate，由它统一申明构造函数。也可以不用父类，由各个类自己实现。
遇到问题：
若父类实现了接口方法：public void WriteMessage(string message)
								  {
										MsgWriter.WriteMessage(message);
								  }
那么decorateMsgWriter.WriteMessage时，当运行到传递时，不会由EncryptedDecorate进行实现，而是由父类实现，并不会进入到EncryptedDecorate.WriteMessage。

