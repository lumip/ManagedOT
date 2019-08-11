# ManagedOT

## Introduction

ManagedOT is a library for *oblivious transport* (OT), fully written in managed C# code with no native dependencies. It targets .NET Standard 1.3 and can therefore be easily deployed on different operating systems.

OT is a cryptographic technique in which a sender offers a number of messages of which a receiver chooses and receives exactly one, without the sender learning which message the receiver chose. It is a fundamental building block in many privacy-preserving applications, e.g., *secure multi-party computation*. 

ManagedOT grew out of the [CompactMPC](https://github.com/jnagykuhlen/CompactMPC) project and is intended to provide more choices for the OT protocol used therein. It can, however, be used as a standalone library providing only OT implementations just as well.

**Note** that the library is currently in early development and everything is subject to sudden and violent changes.

## Features

This library implements several flavors of OT, including the famous protocol by Naor and Pinkas [1], the *OT extension* protocol of Ishai et al. [2] and the *Random-OT* and *Correlated-OT* variants introduced by Asharov et al. [3]. The main goal of this project is to provide a fully managed implementation with a clean and extensible API that easily allows to tweak security parameters and exchange individual components, such as the underlying network layer or cryptographic primitives. It is thus intended to serve as a basis for everyone intending to get familiar with privacy-preserving computation in general and oblivious transfer protocols in specifics.

The following design decisions guide the project:

- No native code. The project is to a hundred percent written in C# and only references .NET Standard libraries. This makes prototyping and deployment on different operating systems an extremely straightforward task.
- Well-defined scope. While the project aims to cover a variety of use cases for OT, it will only provide a single solution for each (that can be parameterized if necessary). Therefore, the user is not required to learn about many underlying cryptographic details in order to make an informed decision. However, the API comes with all necessary abstractions that allow users to extend and add functionality where needed.
- Maintainability before performance. While striving to provide high-performant OT implementations, this project puts a focus on having a clean and maintainable codebase. This can, at times, come at the cost of performance optimization, if these would lead to more convoluated code. In consequence, this library might not be on par with existing OT solutions performance-wise. However, there are probably many application areas in which a clean codebase is favored over raw speed. This is especially true when it comes to prototyping.

## License

This project is published under the [MIT license](/LICENSE).

## References

[1] [Moni Naor and Benny Pinkas: Efficient oblivious transfer protocols 2001.](https://dl.acm.org/citation.cfm?id=365502)

[2] [Yuval Ishai, Joe Kilian, Kobbi Nissim and Erez Petrank: Extending Oblivious Transfers Efficiently. 2003.](https://link.springer.com/content/pdf/10.1007/978-3-540-45146-4_9.pdf) 

[3] [Gilad Asharov, Yehuda Lindell, Thomas Schneider and Michael Zohner: More Efficient Oblivious Transfer and Extensions for Faster Secure Computation. 2013.](https://thomaschneider.de/papers/ALSZ13.pdf) 

