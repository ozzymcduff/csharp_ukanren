module MicroKanren
  module Lisp

    def cons(x,y)
      Cons.new(x,y)
    end

    def car(z)    ; z.car    ; end
    def cdr(z)    ; z.cdr    ; end

    def cons?(d)
      d.respond_to?(:ccel?) && d.ccel?
    end
    alias :pair? :cons?

    def map(func, list)
      cons(func.call(car(list)), map(func, cdr(list))) if list
    end

    def length(list)
      list.nil? ? 0 : 1 + length(cdr(list))
    end

    # We implement scheme cons cells as Procs. This function returns a boolean
    # identically to the Scheme procedure? function to avoid false positives.
    def procedure?(elt)
      elt.is_a?(Proc) && !cons?(elt)
    end

    # Search association list by predicate function.
    # Based on lua implementation by silentbicycle:
    # https://github.com/silentbicycle/lua-ukanren/blob/master/ukanren.lua#L53:L61
    #
    # Additional reference for this function is scheme:
    # Ref for assp: http://www.r6rs.org/final/html/r6rs-lib/r6rs-lib-Z-H-4.html
    def assp(func, alist)
      if alist
        first_pair  = car(alist)
        first_value = car(first_pair)

        if func.call(first_value)
          first_pair
        else
          assp(func, cdr(alist))
        end
      else
        false
      end
    end

    # Converts Lisp AST to a String. Algorithm is a recursive implementation of
    # http://www.mat.uc.pt/~pedro/cientificos/funcional/lisp/gcl_22.html#SEC1238.
    def lprint(node, cons_in_cdr = false)
      if cons?(node)
        str = cons_in_cdr ? '' : '('
        str += lprint(car(node))

        if cons?(cdr(node))
          str += ' ' + lprint(cdr(node), true)
        else
          str += ' . ' + lprint(cdr(node)) unless cdr(node).nil?
        end

        cons_in_cdr ? str : str << ')'
      else
        atom_string(node)
      end
    end

    def lists_equal?(a, b)
      if cons?(a) && cons?(b)
        lists_equal?(car(a), car(b)) && lists_equal?(cdr(a), cdr(b))
      else
        a == b
      end
    end

    private

    def atom_string(node)
      case node
      when NilClass, Array, String
        node.inspect
      else
        node.to_s
      end
    end

  end
end
