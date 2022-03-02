***

# SOLIDの原則とは？

SOLIDは

- 変更に強い
- 理解しやすい
などのソフトウェアを作ることを目的とした原則です。

次の5つの原則があります。

- Single Responsibility Principle (単一責任の原則)
- Open-Closed Principle (オープン・クローズドの原則)
- Liskov Substitution Principle (リスコフの置換原則)
- Interface Segregation Principle (インタフェース分離の原則)
- Dependency Inversion Principle (依存関係逆転の原則)

上記5つの原則の頭文字をとって**SOLID**の原則と言います。
今回の記事では**Open-Closed Principle (オープン・クローズドの原則)**について解説します。
その他の原則に関しては下記参照。
[Single Responsibility Principle (単一責任の原則)](https://qiita.com/k2491p/items/af380369b7060a2f8ac5)
※その他随時追加予定！

# 簡単に言うと...

「**新しい機能追加のときに元のソースを触らなくていいように設計しよう**」ということです。
その方が**変更に強く理解しやすいコード**になるというわけです。
新しい機能を追加して、その機能はうまく動くようになったけど、その機能追加により他の正常だったはずの機能で不具合が起きるというのはよくある話です。

具体例を用いながら詳しく見ていきましょう。

# オープン・クローズドの原則に反した例
お店のポイントカードを例に考えてみましょう。

***
【仕様】
GetPointというメソッドを作ります。
購入価格の1%をポイントとして計算します。

***

```c#:Point.cs
public sealed class Point
{
	public int GetPoint(int price)
	{
		return (int)(price * 0.01);
	}
}
```

仕様変更を加えます。
会員のランクによってポイントを切り替えてみましょう。


***
【仕様】
GetPointというメソッドを作ります。
会員のランクによって取得ポイントを切り替えます。

会員のランクは会員Noの頭文字で判別します。

- G→ゴールド会員
  - 購入価格の3％ポイント
- S→シルバー会員
  - 購入価格の2％ポイント
- その他→通常会員
  - 購入価格の1％ポイント

***



```c#:Point.cs
public sealed class Point
{
    public int GetPoint(int price, string memberNo)
    {
        // ゴールド会員
        if (memberNo.StartsWith("G"))
        {
            return (int)(price * 0.03);
        }

        // シルバー会員
        if (memberNo.StartsWith("S"))
        {
            return (int)(price * 0.02);
        }

        // 通常会員
        return (int)(price * 0.01);

    }
}
```

会員No(memberNo)を引数にとり、処理を切り替えることでポイントを取得できるようになりました。

なんとなく書いてしまいそうなソースですが、下記のような問題をはらんでいます。



# 責任が単一ではない例の問題点及び改善点

例えば新たにプラチナ会員を入れるとします。

GetPointメソッドに新たにif文を追加して処理を追加していくことになるでしょう。

もし仕様をあまり知らないメンバーがプラチナ会員を追加することになって、既存のソースを誤って修正してしまったら、例えば、ゴールド会員の`price * 0.03`という処理を誤って触ってしまったとします。

そのメンバーはプラチナ会員の処理のチェックをするだけで満足して実装を終え、ゴールド会員の誤って修正には気付かないかもしれません。

上記のようなシンプルなソースなら起こり得ないかもしれませんが、もう少しロジックが複雑になると、上記のような「ありえないミス」が起こるかもしれません。

この根本的な原因が「**新しい機能追加のときに元のソースやロジックを触らなければならない設計になっている**」ことです。

この問題を解決するために下記の3ステップで改善していきます。

1. クラスを分ける
2. インターフェースを作る
3. ファクトリーを作る

## 1. クラスを分ける

「**新しい機能追加のときに元のソースやロジックを触らなければならない設計になっている**」という問題があるので、まずはロジックを1つずつクラスとして分けてみましょう。

尚、会員ごとにクラスを分けていくため、会員判別処理はクライアントコード(呼び出し元)に移植します。

```c#:NormalMemberPoint.cs
public sealed class NormalMemberPoint
{
    public int GetPoint(int price)
    {
    	return (int)(price * 0.01);
    }
}
```

```c#:SilverMemberPoint.cs
public sealed class SilverMemberPoint
{
    public int GetPoint(int price)
    {
        return (int)(price * 0.02);
    }
}
```

```c#:GoldMemberPoint.cs
public sealed class GoldMemberPoint
{
    public int GetPoint(int price)
    {
        return (int)(price * 0.03);
    }
}
```

例えばプラチナ会員を追加したいときは新たに`PlatinumMemberPoint`というクラスを実装すれば良いので、元のソースをいじらなくてよくなったように見えます。

しかし、クライアントコード(呼び出し元)で、会員によって呼び出すクラスを変えるというロジックを書く必要が出てきます。

```c#:Program.cs
class Program
{
    static void Main(string[] args)
    {
        var memberNo = args[0].ToString();
        int price = Convert.ToInt32(args[1]);

        if (memberNo.StartsWith("G"))
        {
            GoldMemberPoint point = new GoldMemberPoint();
            Console.WriteLine($"メンバー:{memberNo}は{price}円で{point.GetPoint(price)}ポイント獲得！");
        }
        else if (memberNo.StartsWith("S"))
        {
            SilverMemberPoint point = new SilverMemberPoint();
            Console.WriteLine($"メンバー:{memberNo}は{price}円で{point.GetPoint(price)}ポイント獲得！");
        }
        else
        {
            NormalMemberPoint point = new NormalMemberPoint();
            Console.WriteLine($"メンバー:{memberNo}は{price}円で{point.GetPoint(price)}ポイント獲得！");
        }
    }
}
```

機能拡張でクラスを追加するたびに、クライアントコードのif文のロジックを触る必要があります。

## 2. インターフェースを作る

この問題を解決するために、まずはPointのInterfaceを導入してみます。

```c#:IPoint.cs
interface IPoint
{
    public int GetPoint(int point);
}
```

それぞれの会員クラスでIPointを継承するようにします。

```c#:NormalMemberPoint.cs
public sealed class NormalMemberPoint : IPoint
{
    public int GetPoint(int price)
    {
    	return (int)(price * 0.01);
    }
}
```

```c#:SilverMemberPoint.cs
public sealed class SilverMemberPoint : IPoint
{
    public int GetPoint(int price)
    {
        return (int)(price * 0.02);
    }
}
```

```c#:GoldMemberPoint.cs
public sealed class GoldMemberPoint : IPoint
{
    public int GetPoint(int price)
    {
        return (int)(price * 0.03);
    }
}
```

これによりクライアントコードが少しスッキリします。

```c#:Program.cs
static void Main(string[] args)
{
    IPoint _point;
    var memberNo = args[0].ToString();
    int price = Convert.ToInt32(args[1]);
    
    if (memberNo.StartsWith("G"))
    {
        _point = new GoldMemberPoint();
    }
    else if (memberNo.StartsWith("S"))
    {
        _point = new SilverMemberPoint();
    }
    else
    {
        _point = new NormalMemberPoint();

    }
    Console.WriteLine($"メンバー:{memberNo}は{price}円で{_point.GetPoint(price)}ポイント獲得！");

}
```

しかし、依然としてクライアントコードが切り分けのロジックを持つ必要があります。

## 3.ファクトリーを作る

クライアントコードのIPointの具象クラスを決定する処理をファクトリーに移行します。

これはデザインパターンのFactory Methodパターンを使用します。

```c#:Factory.cs
static class Factory
{
    public static IPoint CreateIPoint(string memberNo)
    {
        if (memberNo.StartsWith("G"))
        {
            return new GoodInterface.GoldMemberPoint();
        }

        if (memberNo.StartsWith("S"))
        {
            return new GoodInterface.SilverMemberPoint();
        }

        return new GoodInterface.NormalMemberPoint();

    }
}
```

このファクトリーの導入によりクライアントコードは、どの具象クラスが呼ばれるのかを知る必要がなくなり、IPointクラスのGetPointメソッドを呼ぶことだけに集中できます。

```c#
static void Main(string[] args)
{
    var memberNo = args[0].ToString();
    int price = Convert.ToInt32(args[1]);
    IPoint _point = Factory.CreateIPoint(memberNo);

    Console.WriteLine($"メンバー:{memberNo}は{price}円で{_point.GetPoint(price)}ポイント獲得！");

}
```

これにより「**新しい機能追加のときに元のソースを触らない**」という当初の目的を達することができました。

プラチナ会員を追加するときは、IPointを継承したプラチナ会員クラスを作り、Factoryを修正するのみとなります。



## おまけ(Abstractクラスを使う)

上記の例ではInterfaceを使って「**新しい機能追加のときに元のソースを触らない**」という目的を達成していました。

Interfaceではなく、Abstractクラスを使って実現することも可能です。

なにか共通の処理を噛ませたいときには、Interfaceを使うよりAbstractクラスを使うのが適しています。

例えば、GetPointメソッドの中に「毎月10日はポイント10倍」という仕様を追加してみましょう。

```c#:PointBase.cs
abstract class PointBase
{
    public int GetPoint(int price)
    {
        var point = GetPointSub(price);
        if (DateTime.Now.Day == 10)
        {
            point *= 10;
        }
        return point;
    }

    protected abstract int GetPointSub(int price);
}
```

IPointというInterfaceをPointBaseというAbstractクラスに変更しました。

クライアントコードからはPointBaseのGetPointメソッドが呼ばれる点はInterfaceの場合と同じです。

しかし、GetPointメソッドの中身の実装が違います。

GetPointSubというメソッドを呼び出しています。

このメソッドは抽象メソッドとして継承したクラスにoverrideでの実装を強制しています。

ここに各クラスでの仕様を実装します。

そしてGetPointSubメソッドで入手したポイントに対して、10日の場合はポイント10倍という処理を実行してreturnしています。

継承したクラスを見ていきます。

```c#:NormalMemberPoint.cs
sealed class NormalMemberPoint : PointBase
{
    protected override int GetPointSub(int price)
    {
        return (int)(price * 0.01);
    }
}
```

```c#:SilverMemberPoint.cs
sealed class SilverMemberPoint : PointBase
{
    protected override int GetPointSub(int price)
    {
        return (int)(price * 0.02);
    }
}
```

```c#:GoldMemberPoint.cs
sealed class GoldMemberPoint : PointBase
{
    protected override int GetPointSub(int price)
    {
        return (int)(price * 0.03);
    }
}
```



# まとめ

オープン・クローズドの原則とは「**新しい機能追加のときに元のソースを触らなくていいように設計しよう**」ということでした。

具体的な手法は

- InterfaceやAbstractクラスを使って抽象化(一般化)したものを作る
- 抽象化(一般化)したものを継承した具象クラスを作る
- クライアントコードはFactory経由でクラス生成を行う

というものでした。

尚、今回使用したソースは[こちら](https://github.com/k2491p/OCP)に上がっています。



# 補足

本記事の中でのオープン・クローズドの原則はざっくりとした理解を補助するもので正確性は欠いています。
正しい説明では

> ソフトウェアの構成要素は拡張に対しては開いていて、修正に対して閉じていなければならない

などと表現されています。
もう少し詳しいことが知りたい場合は、参考文献にあげているような本や記事を読んでみてください。

# 参考文献

- 本
  - [Clean Architecture 達人に学ぶソフトウェアの構造と設計](https://www.amazon.co.jp/Clean-Architecture-達人に学ぶソフトウェアの構造と設計-Robert-C-Martin/dp/4048930656)
- サイト
  - [SOLID原則を勉強する その2 ～オープン・クローズド原則(OCP)～](https://qiita.com/riekure/items/41c891c50a868cfd5939)
  - [きれいなコードを書くためにSOLID原則を学びました② ~オープン・クローズドの原則~](https://qiita.com/suzuki0430/items/b0e2a2c294d81ecdefba)
  - [【SOLID原則】オープン・クローズドの原則 - OCP](https://zenn.dev/chida/articles/d859839928a39d)
  - [よくわかるSOLID原則2: O（オープン・クローズドの原則）](https://note.com/erukiti/n/n959277a74dd0)
- 動画
  - [オブジェクト指向の原則１：単一責務の原則とオープンクローズドの原則](https://www.udemy.com/course/objectfive1/)