using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Kapitalist.Data.Store.Models
{
    [Table("CPV")]
    public class ClassificationCPV : ClassificationMultilingual
    {
        [Required]
        [StringLength(10)]
        public override string Id { get; set; }
    }

    [ComplexType]
    public class ClassificationCPVOptional : BaseSingleClassification, IComplexType, IClassification, IEquatable<ClassificationCPVOptional>
    {
        public ClassificationCPVOptional()
        {
        }

        public ClassificationCPVOptional(IClassification classification)
        {
            Id = classification?.Id;
            Description = classification?.Description;
            if (classification != null)
            {
                if (classification.Uri != null)
                    Trace.TraceWarning("CPV classification uri is not empty: {0}.", classification.Uri);
                if (!"CPV".Equals(classification.Scheme, StringComparison.OrdinalIgnoreCase))
                    Trace.TraceWarning("CPV classification scheme {0} is wrong.", classification.Scheme);
            }
        }

        [StringLength(32)]
        public override string Id { get; set; }

        [NotMapped]
        public string Scheme
        {
            get {
                return "CPV";
            }

            set {
                throw new NotImplementedException("Scheme cannot be changed for CPV classification");
            }
        }

        [NotMapped]
        public string Uri
        {
            get {
                return null;
            }

            set {
                throw new NotImplementedException("Uri cannot be changed for CPV classification");
            }
        }

        public bool Equals(ClassificationCPVOptional other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClassificationCPVOptional)obj) && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0) + base.GetHashCode();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Id)
                && string.IsNullOrEmpty(Description);
        }
    }
}