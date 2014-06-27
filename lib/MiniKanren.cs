using System;

namespace MicroKanren{

  public class Cons<T1,T2>{
    //attr_reader :car, :cdr
    public T1 Car{get;private set;}
    public T2 Cdr{get;private set;}
    //# Returns a Cons cell (read: instance) that is also marked as such for
    //# later identification.
    public Cons(T1 car,T2 cdr){ //def initialize(car, cdr)
      Car=car; Cdr = cdr;
    }//end

    //# Converts Lisp AST to a String. Algorithm is a recursive implementation of
    //# http://www.mat.uc.pt/~pedro/cientificos/funcional/lisp/gcl_22.html#SEC1238.
    /*def to_s(cons_in_cdr = false)
      str = cons_in_cdr ? '' : '('

      str += self.car.is_a?(Cons) ? self.car.to_s : atom_string(self.car)

      str += case self.cdr
      when Cons
        ' ' + self.cdr.to_s(true)
      when NilClass
        ''
      else
        ' . ' + atom_string(self.cdr)
      end

      cons_in_cdr ? str : str << ')'
    end*/

    /*def ==(other)
      other.is_a?(Cons) ? self.car == other.car && self.cdr == other.cdr : false
    end*/

    //private

    /*def atom_string(node)
      case node
      when NilClass, Array, String
        node.inspect
      else
        node.to_s
      end
    end*/
  
}
}
