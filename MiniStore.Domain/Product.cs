using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniStore.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public bool Active { get; private set; }

        private List<TraitValue> _traits;

        public Product(Guid id, string name)
        {
            Id = id;
            Name = name;
            _traits = new List<TraitValue>();
        }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public void ChangePrice(decimal price)
        {
            Price = price;
        }

        public IReadOnlyCollection<TraitValue> GetTraits()
        {
            return _traits.AsReadOnly();
        }

        public void AddTrait(TraitValue value)
        {
            if (_traits.Any(x => x.TraitDefinitionId == value.TraitDefinitionId))
            {
                throw new DuplicateObjectException();
            }

            _traits.Add(value);
        }

        public void RemoveTrait(TraitDefinition traitDefinition)
        {
            var traitValue = _traits.FirstOrDefault(x => x.TraitDefinitionId == traitDefinition.Id);
            if (traitValue == null)
            {
                throw new ObjectNotFoundException();
            }

            _traits.Remove(traitValue);
        }
    }

    public abstract class TraitDefinition
    {
        public Guid Id { get; }
        public string Name { get; }

        public TraitDefinition(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class NumericTraitDefinition : TraitDefinition
    {
        public NumericTraitDefinition(Guid id, string name) : base(id, name)
        {
        }
    }

    public class TextTraitDefinition : TraitDefinition
    {
        public TextTraitDefinition(Guid id, string name) : base(id, name)
        {
        }
    }

    public class ListTraitDefinition : TraitDefinition
    {
        public IReadOnlyCollection<string> Values => _values.AsReadOnly();
        private List<string> _values;

        public ListTraitDefinition(Guid id, string name, IEnumerable<string> possibleValues) : base(id, name)
        {
            _values = possibleValues.ToList();
        }

        public void AddValue(string value)
        {
            _values.Add(value);
        }

        public void RemoveValue(string value)
        {
            _values.Remove(value);
        }
    }

    public abstract class TraitValue
    {
        public Guid TraitDefinitionId { get; }
        public string Name { get; }

        public TraitValue(TraitDefinition traitDefinition)
        {
            TraitDefinitionId = traitDefinition.Id;
            Name = traitDefinition.Name;
        }
    }

    public class NumericTrait : TraitValue
    {
        public double Value { get; }

        public NumericTrait(NumericTraitDefinition traitDefinition, double value) : base(traitDefinition)
        {
            Value = value;
        }
    }

    public class TextTrait : TraitValue
    {
        public string Value { get; }

        public TextTrait(TextTraitDefinition traitDefinition, string value) : base(traitDefinition)
        {
            Value = value;
        }
    }


    public class CategoryPromoteException : Exception
    {

    }

    public class CategoryRelationshipException : Exception
    {

    }

    public class EntityNotFoundException : Exception
    {

    }
}