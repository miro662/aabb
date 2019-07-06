using Zenject;

namespace AABB
{
    public class PhysicsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISpatialCollection<Collider>>().To<SimpleSpatialCollection<Collider>>().AsTransient();
            Container.Bind<World>().AsSingle();
        }
    }
}