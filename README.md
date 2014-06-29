# microKanren in C#

[microKanren](http://webyrd.net/scheme-2013/papers/HemannMuKanren2013.pdf) is a
minimalist relational (logic) programming language. This project is a port of
microKanren to C#. It is an almost exact translation of
[the original implementation](https://github.com/jasonhemann/microKanren),
which was written for [Petite Chez Scheme](http://www.scheme.com/petitechezscheme.html).

## Installation

To install MicroKanren, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package MicroKanren

## Usage

The following example demonstrates how MicroKanren can be used from a simple program:

```csharp
using 'MicroKanren'
class SimpleProgram::Core{
    public string Do(){
        var res = CallFresh((q)=> Eq(q, 5) )(EmptyState);
        return res.ToString();
    }
}

> new SimpleProgram().Do()
(((([0] . 5)) . 1))
```

See the
[spec file](https://github.com/wallymathieu/csharp_ukanren/blob/master/UKanren/Tests/Describe_Core.cs)
for more examples. The spec file is almost an exact port of the [microKanren tests
written in Scheme](https://github.com/jasonhemann/microKanren/blob/master/microKanren-test.scm).

## Credits

The code in this gem is closely based on the following sources:

* The [microKanren paper](http://webyrd.net/scheme-2013/papers/HemannMuKanren2013.pdf)
  by Jason Hemann and Daniel P. Friedman. I read this paper a couple of times, and
  will probably have to read it a few more before I have a better understanding
  about what this code is doing and how to write more effective logic programs.
* This code is also in parts copied from
  [Scott Vokes' port of microKanren to Lua](https://github.com/silentbicycle/lua-ukanren).
  It was great to have the Lua code as a second example of the implementation in
  the paper, and it made my job especially easy since Lua is so similar to Ruby.

## Dependencies

This project requires Ruby 2.0 or higher. You can see which Rubies work with
this project in the [Travis CI Build Status](https://travis-ci.org/jsl/ruby_ukanren).

## Contributing

1. Fork it ( http://github.com/jsl/ruby_ukanren/fork )
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request

## License

See LICENSE.txt.

## Author

See credits for source of original code. This is a port of a port written by [Justin Leitgeb]
(http://justinleitgeb.com). This port is written by Oskar Gewalli.

## Build Status

[![Build Status](https://travis-ci.org/jsl/ruby_ukanren.png)](https://travis-ci.org/jsl/ruby_ukanren)
