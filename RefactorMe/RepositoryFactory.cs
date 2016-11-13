using RefactorMe.DontRefactor.Data.Implementation;

namespace RefactorMe
{
    public static class RepositoryFactory
    {
        public static dynamic GetRepository(object repo)
        {
            var lr = repo as LawnmowerRepository;

            if (lr != null)
                return lr;

            var pr = repo as PhoneCaseRepository;

            if (pr != null)
                return pr;

            var tr = repo as TShirtRepository;

            if (tr != null)
                return tr;

            return null;
        }
    }
}