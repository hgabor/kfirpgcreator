using System;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.corelib {
	public interface Script {
		Entity Owner { get; set; }
		object[] Run();
	}
}
