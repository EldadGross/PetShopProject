
using PetShop.Data.Model;

namespace PetShop.Client
{
    public class AnimalViewModel
    {
        public IEnumerable<Animal>? Animals { get; set; }
        public int CategoryID { get; set; }
    }
}
