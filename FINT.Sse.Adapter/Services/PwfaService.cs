﻿using System.Collections.Generic;
using System.Linq;
using Fint.Event.Model;
using Fint.Pwfa.Model;
using Fint.Relation.Model;

namespace Fint.Sse.Adapter.Services
{
    public class PwfaService : IPwfaService
    {
        private IEnumerable<Owner> _owners;
        private IEnumerable<Dog> _dogs;

        public PwfaService()
        {
            SetupPwfaData();
        }
        
        public void GetAllDogs(Event<object> serverSideEvent)
        {
            var relationOwner1 = new RelationBuilder()
                .With(Dog.Relasjonsnavn.OWNER)
                .ForType("no.fint.pwfa.model.Owner") //TODO
                .Value("10")
                .Build();

            var relationOwner2 = new RelationBuilder()
                .With(Dog.Relasjonsnavn.OWNER)
                .ForType("no.fint.pwfa.model.Owner") //TODO
                .Value("20")
                .Build();

            var dogs = Enumerable.ToList<Dog>(_dogs);

            serverSideEvent.Data.Add(FintResource<Dog>.With(dogs[0]).AddRelasjoner(relationOwner1));
            serverSideEvent.Data.Add(FintResource<Dog>.With(dogs[1]).AddRelasjoner(relationOwner2));

        }

        public void GetDog(Event<object> serverSideEvent)
        {
            var dog = Enumerable.FirstOrDefault<Dog>(_dogs, d => d.Id.Equals(serverSideEvent.Query));

            var relation = new RelationBuilder()
                .With(Dog.Relasjonsnavn.OWNER)
                .ForType("no.fint.pwfa.model.Owner") //TODO
                .Value($"{dog?.Id}0")
                .Build();

            var resource = FintResource<Dog>
                .With(dog)
                .AddRelasjoner(relation);

            serverSideEvent.Data.Add(resource);

        }

        public void GetAllOwners(Event<object> serverSideEvent)
        {
            var relationDog1 = new RelationBuilder()
                .With(Owner.Relasjonsnavn.DOG)
                .ForType("no.fint.pwfa.model.Dog") //TODO
                .Value("1")
                .Build();

            var relationDog2 = new RelationBuilder()
                .With(Owner.Relasjonsnavn.DOG)
                .ForType("no.fint.pwfa.model.Dog") //TODO
                .Value("2")
                .Build();

            var owners = Enumerable.ToList<Owner>(_owners);

            serverSideEvent.Data.Add(FintResource<Owner>.With(owners[0]).AddRelasjoner(relationDog1));
            serverSideEvent.Data.Add(FintResource<Owner>.With(owners[1]).AddRelasjoner(relationDog2));

        }

        public void GetOwner(Event<object> serverSideEvent)
        {
            var owner = Enumerable.FirstOrDefault<Owner>(_owners, o => o.Id.Equals(serverSideEvent.Query));

            var relation = new RelationBuilder()
                .With(Owner.Relasjonsnavn.DOG)
                .ForType("no.fint.pwfa.model.Dog") //TODO
                .Value($"{owner?.Id.Substring(0, 1)}")
                .Build();

            var resource = FintResource<Owner>
                .With(owner)
                .AddRelasjoner(relation);

            serverSideEvent.Data.Add(resource);

        }
        
        private void SetupPwfaData()
        {
            _owners = new List<Owner>
            {
                new Owner
                {
                    Id = "10",
                    Name = "Mickey Mouse"
                },
                new Owner
                {
                    Id = "20",
                    Name = "Minne Mouse"
                }
            };

            _dogs = new List<Dog>
            {
                new Dog {Id = "1", Name = "Pluto", Breed = "Working Springer Spaniel"},
                new Dog {Id = "2", Name = "Lady", Breed = "Working Springer Spaniel"}
            };
        }

    }
}
